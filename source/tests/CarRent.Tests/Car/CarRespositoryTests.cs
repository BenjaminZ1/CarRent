using CarRent.Car.Domain;
using CarRent.Car.Infrastructure;
using CarRent.Common.Application;
using CarRent.Common.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRent.Tests.Car
{
    [SetUpFixture]
    public class SetupClass
    {
        private static DbContextOptions<BaseDbContext> _options;

        [OneTimeSetUp]
        public void BaseDbContext_CreateDb()
        {
            _options = new DbContextOptionsBuilder<BaseDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;

            using var context = new BaseDbContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static void ResetDb()
        {
            using var context = new BaseDbContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }

    [TestFixture]
    class CarRespositoryTests
    {
        private DbContextOptions<CarDbContext> _options;

        [OneTimeSetUp]
        public void CarDbContext_BuildDbContext()
        {
            SetupClass.ResetDb();

            _options = new DbContextOptionsBuilder<CarDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
        }

        [SetUp]
        public void ResetDb()
        {
            SetupClass.ResetDb();
        }

        private void AddDbTestEntries()
        {
            var carClassFactory = new CarClassFactory();

            using var context = new CarDbContext(_options);
            context.Car.Add(new CarRent.Car.Domain.Car
            {
                Brand = "TestBrand",
                Model = "TestModel",
                Type = "TestType",
                Specification = new CarSpecification
                {
                    EngineDisplacement = 1299,
                    EnginePower = 150,
                    Year = 2015
                },
                Class = carClassFactory.GetCarClass(1)
            });
            context.SaveChanges();
        }

        [Test]
        public async Task Save_WhenNew_ReturnsCorrectResult()
        {
            //arrange
            ResponseDto expectedResult = new ResponseDto
            {
                Flag = true,
                Id = 1,
                Message = "Has Been Added.",
                NumberOfRows = 2
            };

            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            var carClassFactory = new CarClassFactory();
            var car = new CarRent.Car.Domain.Car
            {
                Brand = "TestBrand",
                Model = "TestModel",
                Type = "TestType",

                Class = carClassFactory.GetCarClass(1)
            };

            //act
            var result = await carRepository.Save(car);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Save_WhenExisting_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            ResponseDto expectedResult = new ResponseDto
            {
                Flag = true,
                Id = 1,
                Message = "Has Been Updated.",
                NumberOfRows = 3
            };

            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            var carClassFactory = new CarClassFactory();
            var car = new CarRent.Car.Domain.Car
            {
                Id = 1,
                Brand = "TestBrandNeu",
                Model = "TestModelNeu",
                Type = "TestTypeNeu",
                Specification = new CarSpecification
                {
                    EngineDisplacement = 1399,
                    EnginePower = 250,
                    Year = 2016
                },
                Class = carClassFactory.GetCarClass(2)
            };

            //act
            var result = await carRepository.Save(car);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            int id = 1;
            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.Get(id);

            //assert
            result.Should().BeOfType(typeof(CarRent.Car.Domain.Car));
            result.Id.Should().Be(id);
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.GetAll();

            //assert
            result.Should().BeOfType(typeof(List<CarRent.Car.Domain.Car>));
            result.Count.Should().Be(1);
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            int id = 1;
            ResponseDto expectedResult = new ResponseDto
            {
                Flag = true,
                Id = 0,
                Message = "Has been Deleted.",
                NumberOfRows = 2

            };

            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.Delete(id);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Delete_NotExistingCar_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            ResponseDto expectedResult = new ResponseDto
            {
                Flag = false,
                Id = 0,
                Message = "Car does not exist.",
                NumberOfRows = 0

            };

            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.Delete(id);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Search_CarBrandAndModel_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            string brand = "TestBrand";
            string model = "TestModel";


            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.Search(brand, model);

            result.Should().BeOfType(typeof(List<CarRent.Car.Domain.Car>));
            result.Count.Should().Be(1);
            result[0].Brand.Should().Be(brand);
            result[0].Model.Should().Be(model);
        }

        [Test]
        public async Task Search_CarBrand_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            string brand = "TestBrand";


            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.Search(brand, null);

            result.Should().BeOfType(typeof(List<CarRent.Car.Domain.Car>));
            result.Count.Should().Be(1);
            result[0].Brand.Should().Be(brand);
        }

        [Test]
        public async Task Search_CarModel_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            string model = "TestModel";


            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.Search(null, model);

            result.Should().BeOfType(typeof(List<CarRent.Car.Domain.Car>));
            result.Count.Should().Be(1);
            result[0].Model.Should().Be(model);
        }

        [Test]
        public async Task Search_WhenNotFound_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            string model = "gibtesnicht";


            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.Search(null, model);

            result.Should().BeOfType(typeof(List<CarRent.Car.Domain.Car>));
            result.Count.Should().Be(0);
        }

    }
}
