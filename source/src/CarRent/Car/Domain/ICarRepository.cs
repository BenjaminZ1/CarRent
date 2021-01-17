using CarRent.Common.Application;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CarRent.Car.Domain
{
    public interface ICarRepository
    {
        Task<Car> GetCar(int? id);
        //IQueryable<Car> GetCars { get; }
        Task<List<Domain.Car>> GetCars();
        Task<List<Domain.Car>> Search(string brand, string model);
        Task<ResponseDto> Save(Car car);
        Task<ResponseDto> Delete(int? id);
    }
}
