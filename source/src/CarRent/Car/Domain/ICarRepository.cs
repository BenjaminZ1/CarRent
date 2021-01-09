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
        Task<List<CarDTO>> GetCars();
        Task<ResponseDTO> Save(Car car);
        Task<ResponseDTO> DeleteAsync(int? id);
    }
}
