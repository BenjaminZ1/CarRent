using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Car.Domain;
using CarRent.Common.Application;
using CarRent.Reservation.Application;
using CarRent.Reservation.Controllers;
using CarRent.User.Application;
using CarRent.User.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace CarRent.Tests.Reservation
{
    [TestFixture]
    class ReservationControllerTests
    {
        private List<ReservationDto> _reservationDtoTestData;
        [OneTimeSetUp]
        public void GenerateTestData()
        {
            _reservationDtoTestData = new List<ReservationDto>()
            {
                new ReservationDto()
                {
                    StartDate = DateTime.Parse("07.02.2021 00:00:00"),
                    EndDate = DateTime.Parse("10.02.2021 00:00:00"),
                    Class = new CarClassDto()
                    {
                        Description = "DescriptionTest",
                        PricePerDay = 10.00m
                    },
                    User = new UserDto()
                    {
                        Name = "NameTest",
                        LastName = "LastNameTest",
                        Street = "StreetTest",
                        Place = "PlaceTest",
                        Plz = "9000"
                    }
                },
                new ReservationDto()
                {
                    StartDate = DateTime.Parse("07.02.2022 00:00:00"),
                    EndDate = DateTime.Parse("10.02.2022 00:00:00"),
                    Class = new CarClassDto()
                    {
                        Description = "DescriptionTest2",
                        PricePerDay = 12.00m
                    },
                    User = new UserDto()
                    {
                        Name = "NameTest2",
                        LastName = "LastNameTest2",
                        Street = "StreetTest2",
                        Place = "PlaceTest2",
                        Plz = "9002"
                    }
                },
                new ReservationDto()
                {
                    StartDate = DateTime.Parse("07.02.2023 00:00:00"),
                    EndDate = DateTime.Parse("10.02.2023 00:00:00"),
                    Class = new CarClassDto()
                    {
                        Description = "DescriptionTest3",
                        PricePerDay = 13.00m
                    },
                    User = new UserDto()
                    {
                        Name = "NameTest3",
                        LastName = "LastNameTest3",
                        Street = "StreetTest3",
                        Place = "PlaceTest3",
                        Plz = "9003"
                    }
                }
            };
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationDtoStub = _reservationDtoTestData[0];
            var reservationController = new ReservationController(reservationServiceFake);

            A.CallTo(() => reservationServiceFake.Get(id)).Returns(reservationDtoStub);

            //act
            var actionResult = await reservationController.Get(id);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<ReservationDto>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Get_WhenIdNull_ReturnsCorrectResult()
        {
            //arrange
            int? id = null;
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationDtoStub = _reservationDtoTestData[0];
            var reservationController = new ReservationController(reservationServiceFake);

            A.CallTo(() => reservationServiceFake.Get(id)).Returns(reservationDtoStub);

            //act
            var actionResult = await reservationController.Get(id);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<ReservationDto>));
            var result = actionResult.Result as BadRequestResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task Get_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationController = new ReservationController(reservationServiceFake);

            A.CallTo(() => reservationServiceFake.Get(id)).Throws(new InvalidOperationException
                ("Ich bin eine TestExcpetion"));


            var actionResult = await reservationController.Get(id);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<ReservationDto>));
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error retrieving data from the database");
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationDtoStubs = _reservationDtoTestData;
            var reservationController = new ReservationController(reservationServiceFake);

            A.CallTo(() => reservationServiceFake.GetAll()).Returns(reservationDtoStubs);

            //act
            var actionResult = await reservationController.GetAll();

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<ReservationDto>>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task GetAll_WhenResponseIsNull_ReturnsCorrectResult()
        {
            //arrange
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationDtoStubs = new List<ReservationDto>();
            var reservationController = new ReservationController(reservationServiceFake);

            A.CallTo(() => reservationServiceFake.GetAll()).Returns(reservationDtoStubs);

            //act
            var actionResult = await reservationController.GetAll();

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<ReservationDto>>));
            var result = actionResult.Result as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task GetAll_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationController = new ReservationController(reservationServiceFake);

            A.CallTo(() => reservationServiceFake.GetAll()).Throws(new InvalidOperationException
                ("Ich bin eine TestExcpetion"));

            var actionResult = await reservationController.GetAll();

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<ReservationDto>>));
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error retrieving data from the database");
        }

        [Test]
        public async Task Save_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var carClassFactory = new CarClassFactory();
            var testReservation = new CarRent.Reservation.Domain.Reservation()
            {
                StartDate = DateTime.Parse("07.02.2021 00:00:00"),
                EndDate = DateTime.Parse("10.02.2021 00:00:00"),
                Class = carClassFactory.GetCarClass(1),
                User = new CarRent.User.Domain.User()
                {
                    Name = "NameTest",
                    LastName = "LastNameTest",
                    Street = "StreetTest",
                    Place = "PlaceTest",
                    Plz = "9000"
                }
            };

            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationController = new ReservationController(reservationServiceFake);
            var responseDtoStub = new ResponseDto();

            A.CallTo(() => reservationServiceFake.Save(testReservation)).Returns(responseDtoStub);

            //act
            var actionResult = await reservationController.Save(testReservation);

            //assert
            actionResult.Should().BeOfType(typeof(OkObjectResult));
            var result = actionResult as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Save_WhenReservationIsNull_ReturnsCorrectResult()
        {
            //arrange
            CarRent.Reservation.Domain.Reservation testReservation = null;
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationController = new ReservationController(reservationServiceFake);

            //act
            var actionResult = await reservationController.Save(testReservation);

            //assert
            actionResult.Should().BeOfType(typeof(BadRequestResult));
            var result = actionResult as BadRequestResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task Save_WhenResponseIsNull_ReturnsCorrectResult()
        {
            //arrange
            var carClassFactory = new CarClassFactory();
            var testReservation = new CarRent.Reservation.Domain.Reservation()
            {
                StartDate = DateTime.Parse("07.02.2021 00:00:00"),
                EndDate = DateTime.Parse("10.02.2021 00:00:00"),
                Class = carClassFactory.GetCarClass(1),
                User = new CarRent.User.Domain.User()
                {
                    Name = "NameTest",
                    LastName = "LastNameTest",
                    Street = "StreetTest",
                    Place = "PlaceTest",
                    Plz = "9000"
                }
            };
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationController = new ReservationController(reservationServiceFake);
            ResponseDto responseDtoStub = null;

            A.CallTo(() => reservationServiceFake.Save(testReservation)).Returns(responseDtoStub);

            //act
            var actionResult = await reservationController.Save(testReservation);

            //assert
            actionResult.Should().BeOfType(typeof(NotFoundResult));
            var result = actionResult as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationController = new ReservationController(reservationServiceFake);
            ResponseDto responseDtoStub = new ResponseDto();

            A.CallTo(() => reservationServiceFake.Delete(id)).Returns(responseDtoStub);

            //act
            var actionResult = await reservationController.Delete(id);

            //assert
            actionResult.Should().BeOfType(typeof(OkObjectResult));
            var result = actionResult as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Delete_WhenIdIsNull_ReturnsCorrectResult()
        {
            //arrange
            int? id = null;
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationController = new ReservationController(reservationServiceFake);
            ResponseDto responseDtoStub = new ResponseDto();

            A.CallTo(() => reservationServiceFake.Delete(id)).Returns(responseDtoStub);

            //act
            var actionResult = await reservationController.Delete(id);

            //assert
            actionResult.Should().BeOfType(typeof(BadRequestResult));
            var result = actionResult as BadRequestResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task Delete_WhenResponseIsNull_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var reservationServiceFake = A.Fake<IReservationService>();
            var reservationController = new ReservationController(reservationServiceFake);
            ResponseDto responseDtoStub = null;

            A.CallTo(() => reservationServiceFake.Delete(id)).Returns(responseDtoStub);

            //act
            var actionResult = await reservationController.Delete(id);

            //assert
            actionResult.Should().BeOfType(typeof(NotFoundResult));
            var result = actionResult as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }
    }
}
