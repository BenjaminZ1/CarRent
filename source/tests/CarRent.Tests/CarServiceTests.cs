using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Car.Domain;
using CarRent.Car.Infrastructure;
using FakeItEasy;
using FluentAssertions;
using MockQueryable.FakeItEasy;
using NUnit.Framework;


namespace CarRent.Tests
{
    [TestFixture]
    class CarServiceTests
    {
        private List<Car.Domain.Car> carTestData;
        [OneTimeSetUp]
        public void GenerateTestData()
        {
            var carClassFactory = new CarClassFactory();
            carTestData = new List<Car.Domain.Car>
            {
                new Car.Domain.Car()
                {
                    Id = 1,
                    Brand = "TestBrand",
                    Class = carClassFactory.GetCarClass(1),
                    Model = "TestModel",
                    Type = "TestType",
                    Specification = new CarSpecification()
                    {
                        Id = 1,
                        EngineDisplacement = 1599,
                        EnginePower = 180,
                        Year = 2017
                    }
                },
                new Car.Domain.Car()
                {
                    Id = 2,
                    Brand = "TestBrand2",
                    Class = carClassFactory.GetCarClass(2),
                    Model = "TestModel2",
                    Type = "TestType2",
                    Specification = new CarSpecification()
                    {
                        Id = 2,
                        EngineDisplacement = 1099,
                        EnginePower = 100,
                        Year = 2007
                    }
                }
            };
        }

        [Test]
        public async Task Get_Car_GetsCalledOnce()
        {
            //arrange
            int id = 1;
            var carStub = carTestData[0];
            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);
            A.CallTo(() => carRepositoryFake.Get(id)).Returns(carStub);

            //act
            var result = await carService.Get(id);

            //assert
            A.CallTo(() => carRepositoryFake.Get(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Get_Car_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var carStub = carTestData[0];
            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);
            A.CallTo(() => carRepositoryFake.Get(id)).Returns(carStub);

            var expectedResult = new CarDto(carStub);

            

            //act
            var result = await carService.Get(id);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetAll_Cars_GetsCalledOnce()
        {
            //arrange
            var carsStub = carTestData;
            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);
            A.CallTo(() => carRepositoryFake.GetAll()).Returns(carsStub);

            //act
            var result = await carService.GetAll();

            //assert
            A.CallTo(() => carRepositoryFake.GetAll()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetAll_Cars_ReturnsCorrectResult()
        {
            //arrange
            var carsStub = carTestData;
            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);
            A.CallTo(() => carRepositoryFake.GetAll()).Returns(carsStub);

            var expectedResult = carsStub.Select(c => new CarDto(c));
            
            //act
            var result = await carService.GetAll();

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
