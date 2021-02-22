using CarRent.Car.Domain;
using CarRent.Common.Application;
using CarRent.Reservation.Application;
using CarRent.Reservation.Domain;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Tests.Reservation
{
    [TestFixture]
    class ReservationServiceTests
    {
        private List<CarRent.Reservation.Domain.Reservation> _reservationTestData;

        [SetUp]
        public void GenerateTestData()
        {
            var carClassFactory = new CarClassFactory();
            _reservationTestData = new List<CarRent.Reservation.Domain.Reservation>()
            {
                new CarRent.Reservation.Domain.Reservation()
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
                },
                new CarRent.Reservation.Domain.Reservation()
                {
                    StartDate = DateTime.Parse("07.02.2022 00:00:00"),
                    EndDate = DateTime.Parse("10.02.2022 00:00:00"),
                    Class = carClassFactory.GetCarClass(1),
                    User = new CarRent.User.Domain.User()
                    {
                        Name = "NameTest2",
                        LastName = "LastNameTest2",
                        Street = "StreetTest2",
                        Place = "PlaceTest2",
                        Plz = "9002"
                    }
                },
                new CarRent.Reservation.Domain.Reservation()
                {
                    StartDate = DateTime.Parse("07.02.2023 00:00:00"),
                    EndDate = DateTime.Parse("10.02.2023 00:00:00"),
                    Class = carClassFactory.GetCarClass(1),
                    User = new CarRent.User.Domain.User()
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
        public async Task Get_WhenOk_GetsCalledOnce()
        {
            //arrange
            int? id = 1;
            var reservationStub = _reservationTestData[0];
            var reservationRepositoryFake = A.Fake<IReservationRepository>();

            var reservationService = new ReservationService(reservationRepositoryFake);
            A.CallTo(() => reservationRepositoryFake.Get(id)).Returns(reservationStub);

            //act
            var result = await reservationService.Get(id);

            //assert
            A.CallTo(() => reservationRepositoryFake.Get(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var reservationStub = _reservationTestData[0];
            var reservationRepositoryFake = A.Fake<IReservationRepository>();

            var reservationService = new ReservationService(reservationRepositoryFake);
            A.CallTo(() => reservationRepositoryFake.Get(id)).Returns(reservationStub);

            var expectedResult = new ReservationDto(reservationStub);

            //act
            var result = await reservationService.Get(id);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetAll_WhenOk_GetsCalledOnce()
        {
            //arrange
            var reservationStubs = _reservationTestData;
            var reservationRepositoryFake = A.Fake<IReservationRepository>();

            var reservationService = new ReservationService(reservationRepositoryFake);
            A.CallTo(() => reservationRepositoryFake.GetAll()).Returns(reservationStubs);

            //act
            var result = await reservationService.GetAll();

            //assert
            A.CallTo(() => reservationRepositoryFake.GetAll()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var reservationStubs = _reservationTestData;
            var reservationRepositoryFake = A.Fake<IReservationRepository>();

            var reservationService = new ReservationService(reservationRepositoryFake);
            A.CallTo(() => reservationRepositoryFake.GetAll()).Returns(reservationStubs);

            var expectedResult = reservationStubs.Select(c => new ReservationDto(c));

            //act
            var result = await reservationService.GetAll();

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Delete_WhenOk_GetsCalledOnce()
        {
            //arrange
            int? id = 1;
            var reservationRepositoryFake = A.Fake<IReservationRepository>();

            ResponseDto responseDto = new ResponseDto();

            var reservationService = new ReservationService(reservationRepositoryFake);
            A.CallTo(() => reservationRepositoryFake.Delete(id)).Returns(responseDto);

            //act
            var result = await reservationService.Delete(id);

            //assert
            A.CallTo(() => reservationRepositoryFake.Delete(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var reservationRepositoryFake = A.Fake<IReservationRepository>();

            ResponseDto responseDto = new ResponseDto();
            var expectedResult = responseDto;

            var reservationService = new ReservationService(reservationRepositoryFake);
            A.CallTo(() => reservationRepositoryFake.Delete(id)).Returns(responseDto);

            //act
            var result = await reservationService.Delete(id);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ResponseDto));
            result.Should().BeEquivalentTo(expectedResult);
        }

    }
}
