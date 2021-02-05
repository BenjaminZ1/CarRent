using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CarRent.Car.Domain;
using CarRent.Car.Infrastructure;
using CarRent.Common.Application;
using CarRent.Common.Infrastructure;
using CarRent.User.Domain;
using CarRent.User.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace CarRent.Tests.Reservation
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
    class UserRepositoryTests
    {
        private DbContextOptions<UserDbContext> _options;
        [OneTimeSetUp]
        public void CarDbContext_BuildDbContext()
        {
            SetupClass.ResetDb();

            _options = new DbContextOptionsBuilder<UserDbContext>()
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
            using var context = new UserDbContext(_options);
            context.User.Add(new User.Domain.User()
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
                NumberOfRows = 3
            };

            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            var carClassFactory = new CarClassFactory();
            var user = new User.Domain.User()
            {
                Name = "NameTest2",
                LastName = "LastNameTest2",
                Street = "StreetTest2",
                Place = "PlaceTest2",
                Plz = "9002",
                Reservation = new CarRent.Reservation.Domain.Reservation()
                {
                    Class = carClassFactory.GetCarClass(2),
                    StartDate = DateTime.Parse("06.02.2021 00:00:00"),
                    EndDate = DateTime.Parse("11.02.2021 00:00:00")
                }
            };

            //act
            var result = await userRepository.Save(user);

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
                NumberOfRows = 1
            };

            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            var carClassFactory = new CarClassFactory();
            var user = new User.Domain.User()
            {
                Id = 1,
                Name = "NameTest2",
                LastName = "LastNameTest2",
                Street = "StreetTest2",
                Place = "PlaceTest2",
                Plz = "9002",
                Reservation = new CarRent.Reservation.Domain.Reservation()
                {
                    Class = carClassFactory.GetCarClass(2),
                    StartDate = DateTime.Parse("06.02.2021 00:00:00"),
                    EndDate = DateTime.Parse("11.02.2021 00:00:00")
                }
            };

            //act
            var result = await userRepository.Save(user);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            int id = 1;
            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Get(id);

            //assert
            result.Should().BeOfType(typeof(User.Domain.User));
            result.Id.Should().Be(id);
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.GetAll();

            //assert
            result.Should().BeOfType(typeof(List<User.Domain.User>));
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
                NumberOfRows = 1
            };

            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Delete(id);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Delete_NotExistingUser_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            ResponseDto expectedResult = new ResponseDto
            {
                Flag = false,
                Id = 0,
                Message = "User does not exist.",
                NumberOfRows = 0

            };

            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Delete(id);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Search_UserBrandAndModel_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            string brand = "TestBrand";
            string model = "TestModel";


            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            //var result = await userRepository.Search();

            //result.Should().BeOfType(typeof(List<Car.Domain.Car>));
            //result.Count.Should().Be(1);
        }
    }
}
