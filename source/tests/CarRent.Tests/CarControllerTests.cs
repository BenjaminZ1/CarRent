using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Car.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace CarRent.Tests
{
    [TestFixture]
    class CarControllerTests
    {
        private List<CarDto> carDtoTestData;
        [OneTimeSetUp]
        public void GenerateTestData()
        {
            carDtoTestData = new List<CarDto>()
            {
                new CarDto()
                {
                    Id = 1,
                    Brand = "TestBrand",
                    Class = new CarClassDto()
                    {
                        Description = "Mehr Komfort und Luxus geht nicht.",
                        PricePerDay = 249.99m

                    },
                    Model = "TestModel",
                    Type = "TestType",
                    Specification = new CarSpecificationDto()
                    {
                        CarSpecificationId = 1,
                        EngineDisplacement = 1599,
                        EnginePower = 180,
                        Year = 2017
                    }
                },
                new CarDto()
                {
                    Id = 2,
                    Brand = "TestBrand2",
                    Class = new CarClassDto()
                    {
                        Description = "Die beste Wahl für den Alltag.",
                        PricePerDay = 149.99m

                    },
                    Model = "TestModel2",
                    Type = "TestType2",
                    Specification = new CarSpecificationDto()
                    {
                        CarSpecificationId = 2,
                        EngineDisplacement = 1099,
                        EnginePower = 100,
                        Year = 2007
                    }
                },
                new CarDto()
                {
                    Id = 3,
                    Brand = "TestBrand3",
                    Class = new CarClassDto()
                    {
                        Description = "Die beste Wahl für den sparsamen Fahrer.",
                        PricePerDay = 69.99m

                    },
                    Model = "TestModel3",
                    Type = "TestType3",
                    Specification = new CarSpecificationDto()
                    {
                        CarSpecificationId = 3,
                        EngineDisplacement = 1599,
                        EnginePower = 180,
                        Year = 2017
                    }
                },
            };
        }

        [Test]
        public async Task Get_Car_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var carServiceFake = A.Fake<ICarService>();
            var carDtoStub = carDtoTestData[0];
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.Get(id)).Returns(carDtoStub);

            //act
            var actionResult = await carController.Get(id);

            //assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Get_WhenReturnIsNull_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var carServiceFake = A.Fake<ICarService>();
            CarDto carDtoStub = null;
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.Get(id)).Returns(carDtoStub);

            //act
            var actionResult = await carController.Get(id);

            //assert
            var result = actionResult.Result as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task Get_WhenIdNull_ReturnsCorrectResult()
        {
            //arrange
            int? id = null;
            var carServiceFake = A.Fake<ICarService>();
            CarDto carDtoStub = null;
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.Get(id)).Returns(carDtoStub);

            //act
            var actionResult = await carController.Get(id);

            //assert
            var result = actionResult.Result as BadRequestResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }
    
        [Test]
        public async Task Get_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.Get(id)).Throws(new InvalidOperationException
                ("Ich bin eine TestExcpetion"));

            //act
            //Func<Task> testFunc = async () =>
            //{
            //    await carController.Get(id);
            //};
            var actionResult = await carController.Get(id);

            //assert
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error retrieving data from the database");
        }

        [Test]
        public async Task GetAll_Cars_ReturnsCorrectResult()
        {
            //arrange
            var carServiceFake = A.Fake<ICarService>();
            var carDtoStub = carDtoTestData;
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.GetAll()).Returns(carDtoStub);

            //act
            var actionResult = await carController.GetAll();

            //assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task GetAll_WhenReturnIsNull_ReturnsCorrectResult()
        {
            //arrange
            var carServiceFake = A.Fake<ICarService>();
            var carDtoStub = new List<CarDto>();
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.GetAll()).Returns(carDtoStub);

            //act
            var actionResult = await carController.GetAll();

            //assert
            var result = actionResult.Result as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task GetAll_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.GetAll()).Throws(new InvalidOperationException
                ("Ich bin eine TestExcpetion"));

            //act
            //Func<Task> testFunc = async () =>
            //{
            //    await carController.Get(id);
            //};
            var actionResult = await carController.GetAll();

            //assert
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error retrieving data from the database");
        }
    }
}
