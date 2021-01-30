using System.Collections.Generic;
using System.Linq;
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
            var data = await _db.Get(id);
            var mappedData = new ReservationDto(data);
            return mappedData;
        }

        public async Task<IEnumerable<ReservationDto>> GetAll()
        {
            var data = await _db.GetAll();
            var mappedData = data.Select(x => (new ReservationDto(x)));
            return mappedData;
        }

        public async Task<ResponseDto> Save(Domain.Reservation reservation)
        {
            var responseDto = await _db.Save(reservation);
            return responseDto;
        }

        public async Task<ResponseDto> Delete(int? id)
        {
            var responseDto = await _db.Delete(id);
            return responseDto;
        }
    }
}
