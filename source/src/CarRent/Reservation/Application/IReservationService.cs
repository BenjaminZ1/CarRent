using CarRent.Common.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRent.Reservation.Application
{
    public interface IReservationService
    {
        Task<ReservationDto> Get(int? id);
        Task<IEnumerable<ReservationDto>> GetAll();
        Task<ResponseDto> Save(Domain.Reservation reservation);
        Task<ResponseDto> Delete(int? id);
    }
}
