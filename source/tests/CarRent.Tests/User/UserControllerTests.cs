using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Car.Controllers;
using CarRent.Car.Domain;
using CarRent.Common.Application;
using CarRent.User.Application;
using CarRent.User.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace CarRent.Tests.User
{
    [TestFixture]
    class UserControllerTests
    {
        private List<UserDto> _userDtoTestData;
        [OneTimeSetUp]
        public void GenerateTestData()
        {
            _userDtoTestData = new List<UserDto>()
            {
                new UserDto()
                {
                    Name = "NameTest",
                    LastName = "LastNameTest",
                    Street = "StreetTest",
                    Place = "PlaceTest",
                    Plz = "9000"
                },
                new UserDto()
                {
                    Name = "NameTest2",
                    LastName = "LastNameTest2",
                    Street = "StreetTest2",
                    Place = "PlaceTest2",
                    Plz = "9002"
                },
                new UserDto()
                {
                    Name = "NameTest3",
                    LastName = "LastNameTest3",
                    Street = "StreetTest3",
                    Place = "PlaceTest3",
                    Plz = "9003"
                }
            };
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var userServiceFake = A.Fake<IUserService>();
            var userDtoStub = _userDtoTestData[0];
            var userController = new UserController(userServiceFake);

            A.CallTo(() => userServiceFake.Get(id)).Returns(userDtoStub);

            //act
            var actionResult = await userController.Get(id);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<UserDto>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Get_WhenIdNull_ReturnsCorrectResult()
        {
            //arrange
            int? id = null;
            var userServiceFake = A.Fake<IUserService>();
            var userDtoStub = _userDtoTestData[0];
            var userController = new UserController(userServiceFake);

            A.CallTo(() => userServiceFake.Get(id)).Returns(userDtoStub);

            //act
            var actionResult = await userController.Get(id);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<UserDto>));
            var result = actionResult.Result as BadRequestResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task Get_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);

            A.CallTo(() => userServiceFake.Get(id)).Throws(new InvalidOperationException
                ("Ich bin eine TestExcpetion"));


            var actionResult = await userController.Get(id);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<UserDto>));
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error retrieving data from the database");
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var userServiceFake = A.Fake<IUserService>();
            var userDtoStubs = _userDtoTestData;
            var userController = new UserController(userServiceFake);

            A.CallTo(() => userServiceFake.GetAll()).Returns(userDtoStubs);

            //act
            var actionResult = await userController.GetAll();

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<UserDto>>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task GetAll_WhenResponseIsNull_ReturnsCorrectResult()
        {
            //arrange
            var userServiceFake = A.Fake<IUserService>();
            var userDtoStubs = new List<UserDto>();
            var userController = new UserController(userServiceFake);

            A.CallTo(() => userServiceFake.GetAll()).Returns(userDtoStubs);

            //act
            var actionResult = await userController.GetAll();

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<UserDto>>));
            var result = actionResult.Result as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task GetAll_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);

            A.CallTo(() => userServiceFake.GetAll()).Throws(new InvalidOperationException
                ("Ich bin eine TestExcpetion"));

            var actionResult = await userController.GetAll();

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<UserDto>>));
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error retrieving data from the database");
        }

        [Test]
        public async Task Search_WhenOneMatchingCarExists_ReturnsCorrectResult()
        {
            //arrange
            string name = "NameTest";
            string lastName = "LastNameTest";
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);

            var expectedResult = _userDtoTestData.Where(u => u.Name == name & u.LastName == lastName);

            A.CallTo(() => userServiceFake.Search(null, name, lastName))
                .Returns(_userDtoTestData.Where(u => u.Name == name & u.LastName == lastName));

            //act
            var actionResult = await userController.Search(null, name, lastName);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<UserDto>>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Search_WhenTwoOrMoreMatchingCarExists_ReturnsCorrectResult()
        {
            //arrange
            string name = "NameTest";
            string lastName = "LastNameTest2";
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);

            var expectedResult = _userDtoTestData.Where(u => u.Name == name | u.LastName == lastName);

            A.CallTo(() => userServiceFake.Search(null, name, lastName))
                .Returns(_userDtoTestData.Where(u => u.Name == name | u.LastName == lastName));

            //act
            var actionResult = await userController.Search(null, name, lastName);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<UserDto>>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Search_WhenNoMatchingCarExists_ReturnsCorrectResult()
        {
            //arrange
            string name = "gibtesnicht";
            string lastName = "gibtesnicht";
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);

            var expectedResult = _userDtoTestData.Where(u => u.Name == name & u.LastName == lastName);

            A.CallTo(() => userServiceFake.Search(null, name, lastName))
                .Returns(_userDtoTestData.Where(u => u.Name == name & u.LastName == lastName));

            //act
            var actionResult = await userController.Search(null, name, lastName);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<UserDto>>));
            var result = actionResult.Result as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task Search_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            string name = "gibtesnicht";
            string lastName = "gibtesnicht";
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);

            A.CallTo(() => userServiceFake.Search(null, name, lastName))
                .Throws(new InvalidOperationException("Ich bin eine TestException"));

            //act
            var actionResult = await userController.Search(null, name, lastName);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<UserDto>>));
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
            var testUser = new CarRent.User.Domain.User()
            {
                Name = "NameTest",
                LastName = "LastNameTest",
                Street = "StreetTest",
                Place = "PlaceTest",
                Plz = "9000",
                Reservation = new CarRent.Reservation.Domain.Reservation()
                {
                    Class = carClassFactory.GetCarClass(1),
                    StartDate = DateTime.Parse("05.02.2021 00:00:00"),
                    EndDate = DateTime.Parse("10.02.2021 00:00:00"),
                    UserRef = 1

                }
            };
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);
            var responseDtoStub = new ResponseDto();

            A.CallTo(() => userServiceFake.Save(testUser)).Returns(responseDtoStub);

            //act
            var actionResult = await userController.Save(testUser);

            //assert
            actionResult.Should().BeOfType(typeof(OkObjectResult));
            var result = actionResult as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Save_WhenUserIsNull_ReturnsCorrectResult()
        {
            //arrange
            CarRent.User.Domain.User testUser = null;
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);

            //act
            var actionResult = await userController.Save(testUser);

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
            var testUser = new CarRent.User.Domain.User()
            {
                Name = "NameTest",
                LastName = "LastNameTest",
                Street = "StreetTest",
                Place = "PlaceTest",
                Plz = "9000",
                Reservation = new CarRent.Reservation.Domain.Reservation()
                {
                    Class = carClassFactory.GetCarClass(1),
                    StartDate = DateTime.Parse("05.02.2021 00:00:00"),
                    EndDate = DateTime.Parse("10.02.2021 00:00:00"),
                    UserRef = 1

                }
            };
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);
            ResponseDto responseDtoStub = null;

            A.CallTo(() => userServiceFake.Save(testUser)).Returns(responseDtoStub);

            //act
            var actionResult = await userController.Save(testUser);

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
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);
            ResponseDto responseDtoStub = new ResponseDto();

            A.CallTo(() => userServiceFake.Delete(id)).Returns(responseDtoStub);

            //act
            var actionResult = await userController.Delete(id);

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
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);
            ResponseDto responseDtoStub = new ResponseDto();

            A.CallTo(() => userServiceFake.Delete(id)).Returns(responseDtoStub);

            //act
            var actionResult = await userController.Delete(id);

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
            var userServiceFake = A.Fake<IUserService>();
            var userController = new UserController(userServiceFake);
            ResponseDto responseDtoStub = null;

            A.CallTo(() => userServiceFake.Delete(id)).Returns(responseDtoStub);

            //act
            var actionResult = await userController.Delete(id);

            //assert
            actionResult.Should().BeOfType(typeof(NotFoundResult));
            var result = actionResult as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }
    }
}
