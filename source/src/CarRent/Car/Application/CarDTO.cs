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
            Id = x.Id;
            Brand = x.Brand;
            Model = x.Model;
            Specification = x.Specification;
        }
    }
}
