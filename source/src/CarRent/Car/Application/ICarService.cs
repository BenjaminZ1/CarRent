using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Common.Application;

namespace CarRent.Car.Application
{
    public interface ICarService
    {
        Task<ResponseDTO> Save(Domain.Car car);
    }
}
