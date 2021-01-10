using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Common.Application;


namespace CarRent.Car.Domain
{
    public interface ICarRepository
    {
        Task<Car> GetCar(int? id);
        //IQueryable<Car> GetCars { get; }
        List<Domain.Car> GetCars();
        Task<ResponseDto> Save(Car car);
        Task<ResponseDto> DeleteAsync(int? id);
    }
}
