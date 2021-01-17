using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Common.Application;

namespace CarRent.Car.Application
{
    public interface ICarService
    {
        //IQueryable<Domain.Car> GetCars();
        Task<CarDto> GetCar(int? id);
        Task<IEnumerable<CarDto>> GetCars();
        Task<IEnumerable<CarDto>> Search(string brand, string model);
        Task<ResponseDto> Save(Domain.Car car);
        Task<ResponseDto> Delete(int? id);
    }
}
