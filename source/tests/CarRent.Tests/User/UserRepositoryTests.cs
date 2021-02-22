using CarRent.Car.Domain;
using CarRent.Common.Application;
using CarRent.Common.Infrastructure;
using CarRent.User.Domain;
using CarRent.User.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRent.Tests.User
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
            context.User.Add(new CarRent.User.Domain.User()
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
            var user = new CarRent.User.Domain.User()
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
            var user = new CarRent.User.Domain.User()
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
            result.Should().BeOfType(typeof(CarRent.User.Domain.User));
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
            result.Should().BeOfType(typeof(List<CarRent.User.Domain.User>));
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

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Delete_WhenNotExisting_ReturnsCorrectResult()
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

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Search_UserIdNameLastName_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            int? id = 1;
            string name = "NameTest";
            string lastName = "LastNameTest";


            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Search(id, name, lastName);

            //assert
            result.Should().BeOfType(typeof(List<CarRent.User.Domain.User>));
            result.Count.Should().Be(1);
            result[0].Id.Should().Be(id);
            result[0].Name.Should().Be(name);
            result[0].LastName.Should().Be(lastName);
        }

        [Test]
        public async Task Search_UserNameLastName_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            string name = "NameTest";
            string lastName = "LastNameTest";


            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Search(null, name, lastName);

            //assert
            result.Should().BeOfType(typeof(List<CarRent.User.Domain.User>));
            result.Count.Should().Be(1);
            result[0].Id.Should().Be(1);
            result[0].Name.Should().Be(name);
            result[0].LastName.Should().Be(lastName);
        }

        [Test]
        public async Task Search_UserLastName_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            string lastName = "LastNameTest";

            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Search(null, null, lastName);

            //assert
            result.Should().BeOfType(typeof(List<CarRent.User.Domain.User>));
            result.Count.Should().Be(1);
            result[0].Id.Should().Be(1);
            result[0].Name.Should().Be("NameTest");
            result[0].LastName.Should().Be(lastName);
        }

        [Test]
        public async Task Search_UserId_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            int? id = 1;


            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Search(id, null, null);

            //assert
            result.Should().BeOfType(typeof(List<CarRent.User.Domain.User>));
            result.Count.Should().Be(1);
            result[0].Id.Should().Be(1);
            result[0].Name.Should().Be("NameTest");
            result[0].LastName.Should().Be("LastNameTest");
        }

        [Test]
        public async Task Search_UserIdName_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            int? id = 1;
            string name = "NameTest";


            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Search(id, name, null);

            //assert
            result.Should().BeOfType(typeof(List<CarRent.User.Domain.User>));
            result.Count.Should().Be(1);
            result[0].Id.Should().Be(id);
            result[0].Name.Should().Be(name);
            result[0].LastName.Should().Be("LastNameTest");
        }

        [Test]
        public async Task Search_UserIdLastName_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            int? id = 1;
            string lastName = "LastNameTest";


            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Search(id, null, lastName);

            //assert
            result.Should().BeOfType(typeof(List<CarRent.User.Domain.User>));
            result.Count.Should().Be(1);
            result[0].Id.Should().Be(id);
            result[0].Name.Should().Be("NameTest");
            result[0].LastName.Should().Be(lastName);
        }

        [Test]
        public async Task Search_UserName_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            string name = "NameTest";


            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Search(null, name, null);

            //assert
            result.Should().BeOfType(typeof(List<CarRent.User.Domain.User>));
            result.Count.Should().Be(1);
            result[0].Id.Should().Be(1);
            result[0].Name.Should().Be(name);
            result[0].LastName.Should().Be("LastNameTest");
        }

        [Test]
        public async Task Search_WhenNotFound_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
            int? id = 2;


            await using var context = new UserDbContext(_options);
            IUserRepository userRepository = new UserRepository(context);

            //act
            var result = await userRepository.Search(id, null, null);

            //assert
            result.Should().BeOfType(typeof(List<CarRent.User.Domain.User>));
            result.Count.Should().Be(0);
        }
    }
}
