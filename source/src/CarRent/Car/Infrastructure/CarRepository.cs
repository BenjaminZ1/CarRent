using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Car.Domain;
using CarRent.Common.Application;

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
                car = await _db.Cars.FindAsync(id);
            }
            return car;
        }

        public IQueryable<Domain.Car> GetCars => _db.Cars;
        public async Task<ResponseDTO> Save(Domain.Car car)
        {
            ResponseDTO responseDto = new ResponseDTO();
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
                entity.Specification = car.Specification;

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

        public async Task<ResponseDTO> DeleteAsync(int? id)
        {
            ResponseDTO responseDto = new ResponseDTO();
            Domain.Car car = await GetCar(id);

            if (car != null)
            {
                try
                {
                    _db.Cars.Remove(car);
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
