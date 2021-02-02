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
        [Test]
        public async Task Get_Car_CarRepositoryGetsCalled()
        {
            //arrange
            var carClassFactory = new CarClassFactory();
            var testData = new List<Car.Domain.Car>
            {
                new Car.Domain.Car()
                {
                    Id = 1,
                    Brand = "TestBrand",
                    Class = carClassFactory.GetCarClass(1),
                    Model = "TestModel",
                    Specification = new CarSpecification()
                    {
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
                    Specification = new CarSpecification()
                    {
                        EngineDisplacement = 1099,
                        EnginePower = 100,
                        Year = 2007
                    }
                }
            };

            var mock = testData.AsEnumerable();
            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);

            A.CallTo(() => carRepositoryFake.GetAll()).Returns(testData);

            //act
            var result = await carService.GetAll();

            //assert
            A.CallTo(() => carRepositoryFake.GetAll()).MustHaveHappenedOnceExactly();




        }
    }
}
