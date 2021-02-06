using CarRent.Car.Domain;
using CarRent.Common.Application;
using CarRent.Reservation.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRent.Reservation.Infrastructure
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDbContext _db;
        public ReservationRepository(ReservationDbContext db)
        {
            _db = db;
        }
        public async Task<Domain.Reservation> Get(int? id)
        {
            var reservation = new Domain.Reservation();
            if (id != null)
            {
                reservation = await _db.Reservation
                    .Include(r => r.Class)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.Id == id);
            }
            return reservation;
        }

        public async Task<List<Domain.Reservation>> GetAll()
        {
            var users = await _db.Reservation
                .Include(r => r.Class)
                .Include(r => r.User)
                .ToListAsync();
            return users;
        }

        public async Task<ResponseDto> Save(Domain.Reservation reservation)
        {
            int rows;
            ResponseDto responseDto = new ResponseDto();
            if (reservation.Id == 0)
            {
                try
                {
                    reservation.Class = await FindCarClass(reservation.ClassRef);
                    reservation.User = await FindUser(reservation.UserRef);
                    await _db.AddAsync(reservation);
                    rows = await _db.SaveChangesAsync();

                    responseDto.Id = reservation.Id;
                    responseDto.Flag = true;
                    responseDto.Message = "Has Been Added.";
                    responseDto.NumberOfRows = rows;
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            else if (reservation.Id != 0)
            {
                Domain.Reservation entity = await Get(reservation.Id);
                entity.Id = reservation.Id;
                entity.StartDate = reservation.StartDate;
                entity.EndDate = reservation.EndDate;
                entity.ClassRef = reservation.ClassRef;

                try
                {
                    rows = await _db.SaveChangesAsync();
                    responseDto.Id = reservation.Id;
                    responseDto.Flag = true;
                    responseDto.Message = "Has Been Updated.";
                    responseDto.NumberOfRows = rows;
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            return responseDto;
        }

        public async Task<ResponseDto> Delete(int? id)
        {
            ResponseDto responseDto = new ResponseDto();
            Domain.Reservation reservation = await Get(id);

            if (reservation != null)
            {
                try
                {
                    _db.Reservation.Remove(reservation);

                    var rows = await _db.SaveChangesAsync();

                    responseDto.Flag = true;
                    responseDto.Message = "Has been Deleted.";
                    responseDto.NumberOfRows = rows;
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            else
            {
                responseDto.Flag = false;
                responseDto.Message = "Reservation does not exist.";
            }

            return responseDto;
        }

        private async Task<CarClass> FindCarClass(int? id)
        {
            var findClass = await _db.Class.SingleOrDefaultAsync(cls =>
                cls.Id == id);

            return findClass;
        }

        private async Task<User.Domain.User> FindUser(int? id)
        {
            var findUser = await _db.User.SingleOrDefaultAsync(u =>
                u.Id == id);

            return findUser;
        }
    }
}
