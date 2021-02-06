using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Car.Controllers;
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
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<CarDto>>));
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error retrieving data from the database");
        }
    }
}
