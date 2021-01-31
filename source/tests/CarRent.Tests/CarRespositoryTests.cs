using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CarRent.Car.Domain;
using CarRent.Car.Infrastructure;
using CarRent.Common.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace CarRent.Tests
{
    [SetUpFixture]
    public class SetupClass
    {
        private IConfigurationRoot _configuration;
        private DbContextOptions<BaseDbContext> _options;

        [OneTimeSetUp]
        public void BaseDbContext_CreateDb()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _options = new DbContextOptionsBuilder<BaseDbContext>()
                .UseMySql(_configuration.GetConnectionString("CarRentTestDatabase"),
                    ServerVersion.AutoDetect(_configuration.GetConnectionString("CarRentTestDatabase")))
                .Options;

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

        [SetUp]
        public void CarDbContext_AddTestEntries()
        {
                _builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = _builder.Build();

            _options = new DbContextOptionsBuilder<CarDbContext>()
                .UseMySql(_configuration.GetConnectionString("CarRentTestDatabase"),
                    ServerVersion.AutoDetect(_configuration.GetConnectionString("CarRentTestDatabase")))
                .Options;

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
        public void Get_RetrieveSpecificCar_ReturnsCorrectResult()
        {
            //arrange
            Car.Domain.Car car = new Car.Domain.Car();

            _builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = _builder.Build();

            _options = new DbContextOptionsBuilder<CarDbContext>()
                .UseMySql(_configuration.GetConnectionString("CarRentTestDatabase"),
                    ServerVersion.AutoDetect(_configuration.GetConnectionString("CarRentTestDatabase")))
                .Options;

            var carClassFactory = new CarClassFactory();
            int id = 1;
            var expectedResult = new Car.Domain.Car
            {
                Id = 1,
                Brand = "TestBrand",
                Model = "TestModel",
                Type = "TestType",
            };
            //    Specification = new CarSpecification
            //    {
            //        Id = 1,
            //        EngineDisplacement = 1299,
            //        EnginePower = 150,
            //        Year = 2015,
            //        CarRef = 1,
                    
            //    },
            //    Class = carClassFactory.GetCarClass(1)
            //};
            //expectedResult.Specification.Car = expectedResult;

            //act
            using var context = new CarDbContext(_options);
            var dbResult = context.Car
                .FirstOrDefaultAsync(c => c.Id == id);

            car.Id = dbResult.Result.Id;
            car.Brand = dbResult.Result.Brand;
            car.Model = dbResult.Result.Model;
            car.Type = dbResult.Result.Type;
            //car.Specification = dbResult.Result.Specification;
            //car.Class = dbResult.Result.Class;
            //car.ClassRef = dbResult.Result.ClassRef;
            //car.ClassId = dbResult.Result.ClassId;


            //assert
            car.Should().BeEquivalentTo(expectedResult);
        }
    }
}
