using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Domain;

namespace CarRent.Car.Application
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public CarSpecification Specification { get; set; }

        public CarDTO(Domain.Car x)
        {
            if(Specification == null)
                this.Specification = new CarSpecification();
            Id = x.Id;
            Brand = x.Brand;
            Model = x.Model;
            Specification.Year = x.Specification.Year;
            Specification.EngineDisplacement = x.Specification.EngineDisplacement;
            Specification.EnginePower = x.Specification.EnginePower;
        }
    }
}
