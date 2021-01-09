﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Domain;
using CarRent.Common.Application;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Car.Application
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _db;

        public CarService(ICarRepository db)
        {
            _db = db;
        }

        public async Task<ResponseDTO> Save(Domain.Car car)
        {
            var responseDto = await _db.Save(car);
            return responseDto;
        }
    }
}
