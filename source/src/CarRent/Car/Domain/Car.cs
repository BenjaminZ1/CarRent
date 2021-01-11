using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Car.Domain
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public virtual CarSpecification Specification { get; set; }

        //public Car()
        //{
        //    if (Specification == null)
        //        this.Specification = new CarSpecification();
        //}
    }
}
