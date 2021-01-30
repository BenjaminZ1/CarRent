using System.Collections.Generic;
using System.Threading.Tasks;
using CarRent.Common.Application;
using CarRent.User.Application;

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
