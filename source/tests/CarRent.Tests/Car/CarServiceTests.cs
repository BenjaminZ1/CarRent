using CarRent.Car.Application;
using CarRent.Car.Domain;
using CarRent.Common.Application;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CarRent.Tests
{
    [TestFixture]
    class CarServiceTests
    {
        private List<Car.Domain.Car> _carTestData;
        [SetUp]
        public void GenerateTestData()
        {
            var carClassFactory = new CarClassFactory();
            _carTestData = new List<Car.Domain.Car>
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
                },
                new Car.Domain.Car()
                {
                    Id = 3,
                    Brand = "TestBrand3",
                    Class = carClassFactory.GetCarClass(3),
                    Model = "TestModel3",
                    Type = "TestType3",
                    Specification = new CarSpecification()
                    {
                        Id = 3,
                        EngineDisplacement = 999,
                        EnginePower = 80,
                        Year = 2005
                    }
                }
            };
        }

        [Test]
        public async Task Get_WhenOk_GetsCalledOnce()
        {
            //arrange
            int? id = 1;
            var carStub = _carTestData[0];
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
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var carStub = _carTestData[0];
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
        public async Task GetAll_WhenOk_GetsCalledOnce()
        {
            //arrange
            var carsStub = _carTestData;
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
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var carsStub = _carTestData;
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
        public async Task Search_WhenOk_GetsCalledOnce()
        {
            //arrange
            var carsStub = _carTestData;
            string brand = "TestBrand";
            string model = "TestModel";
            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);
            A.CallTo(() => carRepositoryFake.Search(brand, model))
                .Returns(carsStub.Where(c => c.Brand == brand & c.Model == model)
                    .ToList());

            //act
            var result = await carService.Search(brand, model);

            //assert
            A.CallTo(() => carRepositoryFake.Search(brand, model)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Search_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var carsStub = _carTestData;
            string brand = "TestBrand";
            string model = "TestModel";
            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            var expectedResult = carsStub.Where(c => c.Brand == brand & c.Model == model)
                .Select(c => new CarDto(c))
                .ToList();


            var carService = new CarService(carRepositoryFake, carClassFactoryFake);
            A.CallTo(() => carRepositoryFake.Search(brand, model))
                .Returns(carsStub.Where(c => c.Brand == brand & c.Model == model)
                    .ToList());

            //act
            var result = await carService.Search(brand, model);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Save_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var carStub = _carTestData[0];
            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();
            ResponseDto responseDto = new ResponseDto();
            var expectedResult = responseDto;

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);
            A.CallTo(() => carRepositoryFake.Save(carStub)).Returns(responseDto);

            //act
            var result = await carService.Save(carStub);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ResponseDto));
        }

        [Test]
        public async Task Save_WhenCarClassIsNull_ReturnsCorrectResult()
        {
            //arrange
            var carStub = _carTestData[0];
            carStub.Class = null;
            carStub.ClassId = 0;

            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            ResponseDto responseDto = new ResponseDto()
            {
                Message = $"CarClass with ID {carStub.ClassId} is not allowed"
            };
            var expectedResult = responseDto;

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);


            //act
            var result = await carService.Save(carStub);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ResponseDto));
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Delete_WhenOk_GetsCalledOnce()
        {
            //arrange
            int? id = 1;
            var carsStub = _carTestData;

            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            ResponseDto responseDto = new ResponseDto();
            var expectedResult = responseDto;

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);
            A.CallTo(() => carRepositoryFake.Delete(id)).Returns(responseDto);

            //act
            var result = await carService.Delete(id);

            //assert
            A.CallTo(() => carRepositoryFake.Delete(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var carsStub = _carTestData;

            var carRepositoryFake = A.Fake<ICarRepository>();
            var carClassFactoryFake = A.Fake<CarClassFactory>();

            ResponseDto responseDto = new ResponseDto();
            var expectedResult = responseDto;

            var carService = new CarService(carRepositoryFake, carClassFactoryFake);
            A.CallTo(() => carRepositoryFake.Delete(id)).Returns(responseDto);


            //act
            var result = await carService.Delete(id);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ResponseDto));
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
