using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Car.Domain;
using CarRent.Common.Application;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Car.Infrastructure
{
    
    public class CarRepository : ICarRepository
    {
        private readonly CarDbContext _db;

        public CarRepository(CarDbContext db)
        {
            _db = db;
        }
        public async Task<Domain.Car> GetCar(int? id)
        {
            var car = new Domain.Car();
            if (id != null)
            {
                car = await _db.Car.FindAsync(id);
            }
            return car;
        }

        //public IQueryable<Domain.Car> GetCars => _db.Cars;

        public List<Domain.Car> GetCars()
        {
            var cars =  _db.Car
                    .Include(car => car.Specification)
                    .ToList();
            return cars;
        }

        public async Task<ResponseDto> Save(Domain.Car car)
        {
            ResponseDto responseDto = new ResponseDto();
            if (car.Id == 0)
            {
                try
                {
                    await _db.AddAsync(car);
                    await _db.SaveChangesAsync();

                    responseDto.Id = car.Id;
                    responseDto.Flag = true;
                    responseDto.Message = "Has Been Added.";
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            else if (car.Id != 0)
            {
                Domain.Car entity = await GetCar(car.Id);
                entity.Id = car.Id;
                entity.Brand = car.Brand;
                entity.Model = car.Model;
                entity.Specification.Year = car.Specification.Year;
                entity.Specification.EngineDisplacement = car.Specification.EngineDisplacement;
                entity.Specification.EnginePower = car.Specification.EnginePower;

                try
                {
                    await _db.SaveChangesAsync();
                    responseDto.Id = car.Id;
                    responseDto.Flag = true;
                    responseDto.Message= "Has Been Updated.";
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            return responseDto;
        }

        public async Task<ResponseDto> DeleteAsync(int? id)
        {
            ResponseDto responseDto = new ResponseDto();
            Domain.Car car = await GetCar(id);

            if (car != null)
            {
                try
                {
                    _db.Car.Remove(car);
                    await _db.SaveChangesAsync();

                    responseDto.Flag = true;
                    responseDto.Message = "Has been Deleted.";
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            else
            {
                responseDto.Flag = false;
                responseDto.Message = "Car does not exist.";
            }

            return responseDto;
        }
    }
}
