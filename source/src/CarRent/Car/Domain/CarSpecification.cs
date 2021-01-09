using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Car.Domain
{
    public class CarSpecification
    {
        [ForeignKey("Car")]
        public int CarSepcificationId { get; set; }
        public int Year { get; set; }
        public int EngineDisplacement { get; set; }
        public int EnginePower { get; set; }
        public virtual Car Car { get; set; }
    }
}
