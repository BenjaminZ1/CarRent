using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Domain;
using CarRent.Common.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Car.Application
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _db;

        public CarService(ICarRepository db)
        {
            _db = db;
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
            var responseDto = await _db.Save(car);
            return responseDto;
        }

        public async Task<ResponseDto> Delete(int? id)
        {
            var responseDto = await _db.Delete(id);
            return responseDto;
        }
    }
}
