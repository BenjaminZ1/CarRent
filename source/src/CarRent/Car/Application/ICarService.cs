using CarRent.Common.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRent.Car.Application
{
    public interface ICarService
    {
        Task<CarDto> GetCar(int? id);
        Task<IEnumerable<CarDto>> GetCars();
        Task<IEnumerable<CarDto>> Search(string brand, string model);
        Task<ResponseDto> Save(Domain.Car car);
        Task<ResponseDto> Delete(int? id);
    }
}
