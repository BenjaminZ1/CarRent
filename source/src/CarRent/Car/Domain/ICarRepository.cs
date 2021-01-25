﻿using CarRent.Common.Application;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CarRent.Car.Domain
{
    public interface ICarRepository
    {
        Task<Car> Get(int? id);
        Task<List<Car>> GetAll();
        Task<List<Car>> Search(string brand, string model);
        Task<ResponseDto> Save(Car car);
        Task<ResponseDto> Delete(int? id);
    }
}
