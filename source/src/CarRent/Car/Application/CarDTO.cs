﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Domain;

namespace CarRent.Car.Application
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public CarSpecificationDto Specification { get; set; }

        public CarDto(Domain.Car x)
        {
            if(Specification == null)
                this.Specification = new CarSpecificationDto();
            Id = x.Id;
            Brand = x.Brand;
            Model = x.Model;
            Type = x.Type;
            Specification.Year = x.Specification.Year;
            Specification.EngineDisplacement = x.Specification.EngineDisplacement;
            Specification.EnginePower = x.Specification.EnginePower;
            Specification.CarSpecificationId = x.Specification.Id;

        }
    }
}
