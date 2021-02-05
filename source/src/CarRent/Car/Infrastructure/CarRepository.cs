﻿using CarRent.Car.Domain;
using CarRent.Common.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Car.Infrastructure
{

    public class CarRepository : ICarRepository
    {
        private readonly CarDbContext _db;

        public CarRepository(CarDbContext db)
        {
            _db = db;
        }
        public async Task<Domain.Car> Get(int? id)
        {
            var car = new Domain.Car();
            if (id != null)
            {
                car = await _db.Car
                    .Include(c => c.Specification)
                    .Include(c => c.Class)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            return car;
        }

        public async Task<List<Domain.Car>> GetAll()
        {
            var cars = await _db.Car
                    .Include(c => c.Specification)
                    .Include(c => c.Class)
                    .ToListAsync();
            return cars;
        }

        public async Task<List<Domain.Car>> Search(string brand, string model)
        {
            IQueryable<Domain.Car> query = _db.Car.Include(c => c.Specification)
                .Include(c => c.Class);

            if (!string.IsNullOrEmpty(brand) & string.IsNullOrEmpty(model))
            {
                query = query.Where(c => c.Brand.Contains(brand));
            }
            else if (!string.IsNullOrEmpty(model) & string.IsNullOrEmpty(brand))
            {
                query = query.Where(c => c.Model.Contains(model));
            }
            else if (!string.IsNullOrEmpty(model) & !string.IsNullOrEmpty(brand))
            {
                query = query.Where(c => c.Brand.Contains(brand) & c.Model.Contains(model));
            }

            return await query.ToListAsync();
        }

        public async Task<ResponseDto> Save(Domain.Car car)
        {
            int rows;
            ResponseDto responseDto = new ResponseDto();
            if (car.Id == 0)
            {
                try
                {
                    car.Class = await FindOrAddNewCarClass(car.Class);
                    await _db.AddAsync(car);
                    rows = await _db.SaveChangesAsync();

                    responseDto.Id = car.Id;
                    responseDto.Flag = true;
                    responseDto.Message = "Has Been Added.";
                    responseDto.NumberOfRows = rows;
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            else if (car.Id != 0)
            {
                Domain.Car entity = await Get(car.Id);
                entity.Id = car.Id;
                entity.Brand = car.Brand;
                entity.Model = car.Model;
                entity.Type = car.Type;
                entity.Specification.Year = car.Specification.Year;
                entity.Specification.EngineDisplacement = car.Specification.EngineDisplacement;
                entity.Specification.EnginePower = car.Specification.EnginePower;

                try
                {
                    entity.Class = await FindOrAddNewCarClass(car.Class);
                    rows = await _db.SaveChangesAsync();
                    responseDto.Id = car.Id;
                    responseDto.Flag = true;
                    responseDto.Message = "Has Been Updated.";
                    responseDto.NumberOfRows = rows;
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            return responseDto;
        }

        public async Task<ResponseDto> Delete(int? id)
        {
            ResponseDto responseDto = new ResponseDto();
            Domain.Car car = await Get(id);

            if (car != null)
            {
                try
                {
                    _db.Car.Remove(car);
                    _db.RemoveRange(car.Specification);

                    var rows = await _db.SaveChangesAsync();

                    responseDto.Flag = true;
                    responseDto.Message = "Has been Deleted.";
                    responseDto.NumberOfRows = rows;
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

        private async Task<CarClass> FindOrAddNewCarClass(CarClass carClass)
        {
            var findClass = await _db.Class.SingleOrDefaultAsync(cls => 
                cls.Id == carClass.Id && 
                cls.Description == carClass.Description &&
                cls.PricePerDay == carClass.PricePerDay);

            return findClass ?? carClass;
        }
}
}
