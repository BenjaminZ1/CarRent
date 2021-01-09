using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Common.Domain;

namespace CarRent.Car.Domain
{
    interface ICarRepository
    {
        Task<Car> GetCar(int? id);
        IQueryable<Car> GetCars { get; }
        Task<Response> Save(Car car);
        Task<Response> DeleteAsync(int? id);
    }
}
