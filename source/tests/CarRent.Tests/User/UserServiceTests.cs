using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Car.Domain;
using CarRent.Common.Application;
using CarRent.User.Application;
using CarRent.User.Domain;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace CarRent.Tests.User
{
    [TestFixture]
    class UserServiceTests
    {
        private List<CarRent.User.Domain.User> _userTestData;

        [SetUp]
        public void GenerateTestData()
        {
            var carClassFactory = new CarClassFactory();
            _userTestData = new List<CarRent.User.Domain.User>()
            {
                new CarRent.User.Domain.User()
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
                        EndDate = DateTime.Parse("10.02.2021 00:00:00")
                    }
                },
                new CarRent.User.Domain.User()
                {
                    Name = "NameTest2",
                    LastName = "LastNameTest2",
                    Street = "StreetTest2",
                    Place = "PlaceTest2",
                    Plz = "9002",
                    Reservation = new CarRent.Reservation.Domain.Reservation()
                    {
                        Class = carClassFactory.GetCarClass(2),
                        StartDate = DateTime.Parse("05.02.2022 00:00:00"),
                        EndDate = DateTime.Parse("10.02.2022 00:00:00")
                    }
                },
                new CarRent.User.Domain.User()
                {
                    Name = "NameTest3",
                    LastName = "LastNameTest3",
                    Street = "StreetTest3",
                    Place = "PlaceTest3",
                    Plz = "9003",
                    Reservation = new CarRent.Reservation.Domain.Reservation()
                    {
                        Class = carClassFactory.GetCarClass(3),
                        StartDate = DateTime.Parse("05.02.2023 00:00:00"),
                        EndDate = DateTime.Parse("10.02.2023 00:00:00")
                    }
                }
            };
        }

        [Test]
        public async Task Get_WhenOk_GetsCalledOnce()
        {
            //arrange
            int? id = 1;
            var userStub = _userTestData[0];
            var userRepositoryFake = A.Fake<IUserRepository>();

            var userService = new UserService(userRepositoryFake);
            A.CallTo(() => userRepositoryFake.Get(id)).Returns(userStub);

            //act
            var result = await userService.Get(id);

            //assert
            A.CallTo(() => userRepositoryFake.Get(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var userStub = _userTestData[0];
            var userRepositoryFake = A.Fake<IUserRepository>();

           var userService = new UserService(userRepositoryFake);
           A.CallTo(() => userRepositoryFake.Get(id)).Returns(userStub);

            var expectedResult = new UserDto(userStub);

            //act
            var result = await userService.Get(id);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetAll_WhenOk_GetsCalledOnce()
        {
            //arrange
            var usersStub = _userTestData;
            var userRepositoryFake = A.Fake<IUserRepository>();

            var userService = new UserService(userRepositoryFake);
            A.CallTo(() => userRepositoryFake.GetAll()).Returns(usersStub);

            //act
            var result = await userService.GetAll();

            //assert
            A.CallTo(() => userRepositoryFake.GetAll()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var usersStub = _userTestData;
            var userRepositoryFake = A.Fake<IUserRepository>();

            var userService = new UserService(userRepositoryFake);
            A.CallTo(() => userRepositoryFake.GetAll()).Returns(usersStub);

            var expectedResult = usersStub.Select(c => new UserDto(c));

            //act
            var result = await userService.GetAll();

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Search_WhenOk_GetsCalledOnce()
        {
            //arrange
            var usersStub = _userTestData;
            int? id = 1;
            string name = "NameTest";
            string lastname = "LastNameTest";
            var userRepositoryFake = A.Fake<IUserRepository>();

            var userService = new UserService(userRepositoryFake);
            A.CallTo(() => userRepositoryFake.Search(id, name, lastname))
                .Returns(usersStub.Where(u => u.Id == id & u.Name == name & u.LastName == lastname)
                    .ToList());

            //act
            var result = await userService.Search(id, name, lastname);

            //assert
            A.CallTo(() => userRepositoryFake.Search(id, name, lastname)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Search_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var usersStub = _userTestData;
            int? id = 1;
            string name = "NameTest";
            string lastname = "LastNameTest";
            var userRepositoryFake = A.Fake<IUserRepository>();

            var expectedResult = usersStub.Where(u => u.Id == id & u.Name == name & u.LastName == lastname)
                .Select(u => new UserDto(u))
                .ToList();

            var userService = new UserService(userRepositoryFake);
            A.CallTo(() => userRepositoryFake.Search(id, name, lastname))
                .Returns(usersStub.Where(u => u.Id == id & u.Name == name & u.LastName == lastname)
                    .ToList());

            //act
            var result = await userService.Search(id, name, lastname);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Save_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var userStub = _userTestData[0];
            var userRepositoryFake = A.Fake<IUserRepository>();

            ResponseDto responseDto = new ResponseDto();
            var expectedResult = responseDto;

            var userService = new UserService(userRepositoryFake);
            A.CallTo(() => userRepositoryFake.Save(userStub)).Returns(responseDto);

            //act
            var result = await userService.Save(userStub);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ResponseDto));
        }

        [Test]
        public async Task Delete_WhenOk_GetsCalledOnce()
        {
            //arrange
            int? id = 1;
            var userRepositoryFake = A.Fake<IUserRepository>();

            ResponseDto responseDto = new ResponseDto();
            var expectedResult = responseDto;

            var userService = new UserService(userRepositoryFake);
            A.CallTo(() => userRepositoryFake.Delete(id)).Returns(responseDto);

            //act
            var result = await userService.Delete(id);

            //assert
            A.CallTo(() => userRepositoryFake.Delete(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int? id = 1;
            var userRepositoryFake = A.Fake<IUserRepository>();

            ResponseDto responseDto = new ResponseDto();
            var expectedResult = responseDto;

            var userService = new UserService(userRepositoryFake);
            A.CallTo(() => userRepositoryFake.Delete(id)).Returns(responseDto);

            //act
            var result = await userService.Delete(id);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ResponseDto));
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
