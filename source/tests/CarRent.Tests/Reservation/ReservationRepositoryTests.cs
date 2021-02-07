using CarRent.Car.Domain;
using CarRent.Common.Application;
using CarRent.Common.Infrastructure;
using CarRent.Reservation.Domain;
using CarRent.Reservation.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    class ReservationRepositoryTests
    {
        private DbContextOptions<ReservationDbContext> _options;
        [OneTimeSetUp]
        public void CarDbContext_BuildDbContext()
        {
            SetupClass.ResetDb();

            _options = new DbContextOptionsBuilder<ReservationDbContext>()
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
            using var context = new ReservationDbContext(_options);
            context.Reservation.Add(new CarRent.Reservation.Domain.Reservation()
            {
                StartDate = DateTime.Parse("07.02.2021 00:00:00"),
                EndDate = DateTime.Parse("10.02.2021 00:00:00"),
                Class = carClassFactory.GetCarClass(1),
                User = new CarRent.User.Domain.User()
                {
                    Name = "NameTest",
                    LastName = "LastNameTest",
                    Street = "StreetTest",
                    Place = "PlaceTest",
                    Plz = "9000"
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
                NumberOfRows = 1
            };

            await using var context = new ReservationDbContext(_options);
            IReservationRepository reservationRepository = new ReservationRepository(context);

            var carClassFactory = new CarClassFactory();
            var reservation = new CarRent.Reservation.Domain.Reservation
            {
                StartDate = DateTime.Parse("08.02.2021 00:00:00"),
                EndDate = DateTime.Parse("11.02.2021 00:00:00"),
                Class = carClassFactory.GetCarClass(1),
                User = new CarRent.User.Domain.User()
                {
                    Name = "NameTest2",
                    LastName = "LastNameTest2",
                    Street = "StreetTest2",
                    Place = "PlaceTest2",
                    Plz = "9002",
                }
            };

            //act
            var result = await reservationRepository.Save(reservation);

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

            await using var context = new ReservationDbContext(_options);
            IReservationRepository reservationRepository = new ReservationRepository(context);

            var carClassFactory = new CarClassFactory();
            var reservation = new CarRent.Reservation.Domain.Reservation
            {
                Id = 1,
                StartDate = DateTime.Parse("08.02.2021 00:00:00"),
                EndDate = DateTime.Parse("11.02.2021 00:00:00"),
                Class = carClassFactory.GetCarClass(1),
                User = new CarRent.User.Domain.User()
                {
                    Name = "NameTest2",
                    LastName = "LastNameTest2",
                    Street = "StreetTest2",
                    Place = "PlaceTest2",
                    Plz = "9002",
                }
            };

            //act
            var result = await reservationRepository.Save(reservation);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            int id = 1;
            await using var context = new ReservationDbContext(_options);
            IReservationRepository reservationRepository = new ReservationRepository(context);

            //act
            var result = await reservationRepository.Get(id);

            //assert
            result.Should().BeOfType(typeof(CarRent.Reservation.Domain.Reservation));
            result.Id.Should().Be(id);
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();

            await using var context = new ReservationDbContext(_options);
            IReservationRepository reservationRepository = new ReservationRepository(context);

            //act
            var result = await reservationRepository.GetAll();

            //assert
            result.Should().BeOfType(typeof(List<CarRent.Reservation.Domain.Reservation>));
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

            await using var context = new ReservationDbContext(_options);
            IReservationRepository reservationRepository = new ReservationRepository(context);

            //act
            var result = await reservationRepository.Delete(id);

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
                Message = "Reservation does not exist.",
                NumberOfRows = 0

            };
            await using var context = new ReservationDbContext(_options);
            IReservationRepository reservationRepository = new ReservationRepository(context);

            //act
            var result = await reservationRepository.Delete(id);

            result.Should().BeEquivalentTo(expectedResult);
        }
    }


}
