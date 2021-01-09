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

        public async Task<List<CarDTO>> GetCars()
        {
            var data = await _db.GetCars();
            return data;
        }

        public async Task<ResponseDTO> Save(Domain.Car car)
        {
            var responseDto = await _db.Save(car);
            return responseDto;
        }
    }
}
