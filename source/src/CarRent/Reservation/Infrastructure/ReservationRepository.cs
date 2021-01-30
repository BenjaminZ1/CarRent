using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRent.Common.Application;
using CarRent.Reservation.Domain;
using Microsoft.EntityFrameworkCore;

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
            var users = await _db.Reservation.ToListAsync();
            return users;
        }

        public async Task<ResponseDto> Save(Domain.Reservation reservation)
        {
            ResponseDto responseDto = new ResponseDto();
            if (reservation.Id == 0)
            {
                try
                {

                    await _db.AddAsync(reservation);
                    await _db.SaveChangesAsync();

                    responseDto.Id = reservation.Id;
                    responseDto.Flag = true;
                    responseDto.Message = "Has Been Added.";
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
                    await _db.SaveChangesAsync();
                    responseDto.Id = reservation.Id;
                    responseDto.Flag = true;
                    responseDto.Message = "Has Been Updated.";
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

                    await _db.SaveChangesAsync();

                    responseDto.Flag = true;
                    responseDto.Message = "Has been Deleted.";
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
    }
}
