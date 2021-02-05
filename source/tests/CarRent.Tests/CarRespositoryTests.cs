using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.Car.Domain;
using CarRent.Car.Infrastructure;
using CarRent.Common.Application;
using CarRent.Common.Infrastructure;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using MockQueryable.FakeItEasy;
using NUnit.Framework;

namespace CarRent.Tests
{
    [SetUpFixture]
    public class SetupClass
    {
        private IConfigurationRoot _configuration;
        private static DbContextOptions<BaseDbContext> _options;

        [OneTimeSetUp]
        public void BaseDbContext_CreateDb()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _options = new DbContextOptionsBuilder<BaseDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
                //.UseMySql(_configuration.GetConnectionString("CarRentTestDatabase"),
                //    ServerVersion.AutoDetect(_configuration.GetConnectionString("CarRentTestDatabase")))
                //.Options;

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
        private IConfigurationRoot _configuration;
        private DbContextOptions<CarDbContext> _options;
        private IConfigurationBuilder _builder;

        [OneTimeSetUp]
        public void CarDbContext_BuildDbContext()
        {
            SetupClass.ResetDb();

            _builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            _configuration = _builder.Build();

            _options = new DbContextOptionsBuilder<CarDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
            //.UseMySql(_configuration.GetConnectionString("CarRentTestDatabase"),
            //    ServerVersion.AutoDetect(_configuration.GetConnectionString("CarRentTestDatabase")))
            //.Options;
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
            context.Car.Add(new Car.Domain.Car
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
                NumberOfRows = 0
            };

            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            var carClassFactory = new CarClassFactory();
            var car = new Car.Domain.Car
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
                NumberOfRows = 0
            };

            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            var carClassFactory = new CarClassFactory();
            var car = new Car.Domain.Car
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
        public async Task Get_Car_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            int id = 1;
            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.Get(id);

            //assert
            result.Should().BeOfType(typeof(Car.Domain.Car));
        }

        [Test]
        public async Task GetAll_Car_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            await using var context = new CarDbContext(_options);
            ICarRepository carRepository = new CarRepository(context);

            //act
            var result = await carRepository.GetAll();

            //assert
            result.Should().BeOfType(typeof(List<Car.Domain.Car>));
            result.Count.Should().Be(1);
        }

        [Test]
        public async Task Delete_Car_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            int id = 1;
            ResponseDto expectedResult = new ResponseDto
            {
                Flag = true,
                Id = 0,
                Message = "Has been Deleted.",
                NumberOfRows = 0

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

            result.Should().BeOfType(typeof(List<Car.Domain.Car>));
            result.Count.Should().Be(1);
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

            result.Should().BeOfType(typeof(List<Car.Domain.Car>));
            result.Count.Should().Be(1);
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

            result.Should().BeOfType(typeof(List<Car.Domain.Car>));
            result.Count.Should().Be(1);
        }
    }
}
