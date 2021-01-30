using System.Collections.Generic;
using System.Threading.Tasks;
using CarRent.Common.Application;
using CarRent.Reservation.Domain;
using CarRent.User.Domain;

namespace CarRent.Reservation.Application
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _db;
        public ReservationService(IReservationRepository db)
        {
            _db = db;
        }
        public async Task<ReservationDto> Get(int? id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ReservationDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto> Save(Domain.Reservation reservation)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto> Delete(int? id)
        {
            throw new System.NotImplementedException();
        }
    }
}
