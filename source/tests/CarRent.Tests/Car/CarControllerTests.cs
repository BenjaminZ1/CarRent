using CarRent.Car.Application;
using CarRent.Car.Controllers;
using CarRent.Car.Domain;
using CarRent.Common.Application;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Tests.Car
{
    [TestFixture]
    class CarControllerTests
    {
        private List<CarDto> _carDtoTestData;
        [OneTimeSetUp]
        public void GenerateTestData()
        {
            _carDtoTestData = new List<CarDto>()
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
                }
            };
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var carServiceFake = A.Fake<ICarService>();
            var carDtoStub = _carDtoTestData[0];
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.Get(id)).Returns(carDtoStub);

            //act
            var actionResult = await carController.Get(id);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<CarDto>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Get_WhenResponseIsNull_ReturnsCorrectResult()
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
            actionResult.Should().BeOfType(typeof(ActionResult<CarDto>));
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
            actionResult.Should().BeOfType(typeof(ActionResult<CarDto>));
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
            actionResult.Should().BeOfType(typeof(ActionResult<CarDto>));
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error retrieving data from the database");
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var carServiceFake = A.Fake<ICarService>();
            var carDtoStubs = _carDtoTestData;
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.GetAll()).Returns(carDtoStubs);

            //act
            var actionResult = await carController.GetAll();

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<CarDto>>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task GetAll_WhenResponseIsNull_ReturnsCorrectResult()
        {
            //arrange
            var carServiceFake = A.Fake<ICarService>();
            var carDtoStubs = new List<CarDto>();
            var carController = new CarController(carServiceFake);

            A.CallTo(() => carServiceFake.GetAll()).Returns(carDtoStubs);

            //act
            var actionResult = await carController.GetAll();

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<CarDto>>));
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
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<CarDto>>));
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error retrieving data from the database");
        }

        [Test]
        public async Task Search_WhenOneMatchingCarExists_ReturnsCorrectResult()
        {
            //arrange
            string brand = "TestBrand";
            string model = "TestModel";
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);

            var expectedResult = _carDtoTestData.Where(c => c.Brand == brand & c.Model == model);

            A.CallTo(() => carServiceFake.Search(brand, model))
                .Returns(_carDtoTestData.Where(c => c.Brand == brand & c.Model == model));

            //act
            var actionResult = await carController.Search(brand, model);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<CarDto>>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Search_WhenTwoOrMoreMatchingCarExists_ReturnsCorrectResult()
        {
            //arrange
            string brand = "TestBrand";
            string model = "TestModel2";
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);

            var expectedResult = _carDtoTestData.Where(c => c.Brand == brand | c.Model == model);

            A.CallTo(() => carServiceFake.Search(brand, model))
                .Returns(_carDtoTestData.Where(c => c.Brand == brand | c.Model == model));

            //act
            var actionResult = await carController.Search(brand, model);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<CarDto>>));
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Search_WhenNoMatchingCarExists_ReturnsCorrectResult()
        {
            //arrange
            string brand = "gibtesnicht";
            string model = "gibtesnicht";
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);

            var expectedResult = _carDtoTestData.Where(c => c.Brand == brand & c.Model == model);

            A.CallTo(() => carServiceFake.Search(brand, model))
                .Returns(_carDtoTestData.Where(c => c.Brand == brand & c.Model == model));

            //act
            var actionResult = await carController.Search(brand, model);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<CarDto>>));
            var result = actionResult.Result as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task Search_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            string brand = "TestBrand";
            string model = "TestModel";
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);


            A.CallTo(() => carServiceFake.Search(brand, model))
                .Throws(new InvalidOperationException("Ich bin eine TestException"));

            //act
            var actionResult = await carController.Search(brand, model);

            //assert
            actionResult.Should().BeOfType(typeof(ActionResult<IEnumerable<CarDto>>));
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
            var testCar = new CarRent.Car.Domain.Car()
            {
                Brand = "TestBrand4",
                Class = carClassFactory.GetCarClass(1),
                Model = "TestModel4",
                Type = "TestType4",
                Specification = new CarSpecification()
                {
                    Id = 1,
                    EngineDisplacement = 1699,
                    EnginePower = 220,
                    Year = 2018
                }
            };
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);
            var responseDtoStub = new ResponseDto();

            A.CallTo(() => carServiceFake.Save(testCar)).Returns(responseDtoStub);

            //act
            var actionResult = await carController.Save(testCar);

            //assert
            actionResult.Should().BeOfType(typeof(OkObjectResult));
            var result = actionResult as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Save_WhenCarIsNull_ReturnsCorrectResult()
        {
            //arrange
            CarRent.Car.Domain.Car testCar = null;
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);

            //act
            var actionResult = await carController.Save(testCar);

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
            var testCar = new CarRent.Car.Domain.Car()
            {
                Brand = "TestBrand4",
                Class = carClassFactory.GetCarClass(1),
                Model = "TestModel4",
                Type = "TestType4",
                Specification = new CarSpecification()
                {
                    Id = 1,
                    EngineDisplacement = 1699,
                    EnginePower = 220,
                    Year = 2018
                }
            };
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);
            ResponseDto responseDtoStub = null;

            A.CallTo(() => carServiceFake.Save(testCar)).Returns(responseDtoStub);

            //act
            var actionResult = await carController.Save(testCar);

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
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);
            ResponseDto responseDtoStub = new ResponseDto();

            A.CallTo(() => carServiceFake.Delete(id)).Returns(responseDtoStub);

            //act
            var actionResult = await carController.Delete(id);

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
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);
            ResponseDto responseDtoStub = new ResponseDto();

            A.CallTo(() => carServiceFake.Delete(id)).Returns(responseDtoStub);

            //act
            var actionResult = await carController.Delete(id);

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
            var carServiceFake = A.Fake<ICarService>();
            var carController = new CarController(carServiceFake);
            ResponseDto responseDtoStub = null;

            A.CallTo(() => carServiceFake.Delete(id)).Returns(responseDtoStub);

            //act
            var actionResult = await carController.Delete(id);

            //assert
            actionResult.Should().BeOfType(typeof(NotFoundResult));
            var result = actionResult as NotFoundResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }
    }
}
