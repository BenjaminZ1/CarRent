﻿using CarRent.Common.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRent.Reservation.Domain
{
    public interface IReservationRepository
    {
        Task<Reservation> Get(int? id);
        Task<List<Reservation>> GetAll();
        Task<ResponseDto> Save(Reservation reservation);
        Task<ResponseDto> Delete(int? id);
    }
}
