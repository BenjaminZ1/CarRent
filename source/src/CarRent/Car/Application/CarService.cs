using System;
using CarRent.Car.Domain;
using CarRent.Common.Application;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Car.Application
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _db;
        private readonly ICarClassFactory _factory;

        public CarService(ICarRepository db, ICarClassFactory factory)
        {
            _db = db;
            _factory = factory;
        }

        //public IQueryable<Domain.Car> GetCars()
        //{
        //    //var data = await _db.GetCars.Select(x => new CarDTO(x)).ToListAsync();
        //    var data =  _db.GetCars;
        //    return data;
        //}
        public async Task<CarDto> GetCar(int? id)
        {
            var data = await _db.GetCar(id);
            var mappedData = new CarDto(data);
            return mappedData;
        }

        public async Task<IEnumerable<CarDto>> GetCars()
        {
            var data = await _db.GetCars();
            var mappedData = data.Select(x => (new CarDto(x)));
            return mappedData;
        }

        public async Task<IEnumerable<CarDto>> Search(string brand, string model)
        {
            var data = await _db.Search(brand, model);
            var mappedData = data.Select(x => (new CarDto(x)));
            return mappedData;
        }

        public async Task<ResponseDto> Save(Domain.Car car)
        {
            car.Class = _factory.GetCarClass(car.ClassId);
            ResponseDto responseDto;

            if (car.Class == null)
            {
                responseDto = new ResponseDto() {Flag = false, Message =
                    $"CarClass with ID {car.ClassId} is not allowed"
                };

                return responseDto;
            }
            
            responseDto = await _db.Save(car);
            return responseDto;
        }

        public async Task<ResponseDto> Delete(int? id)
        {
            var responseDto = await _db.Delete(id);
            return responseDto;
        }
    }
}
