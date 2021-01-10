using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Car.Domain
{
    public class CarSpecification
    {
        public int CarSpecificationId { get; set; }
        public int Year { get; set; }
        public int EngineDisplacement { get; set; }
        public int EnginePower { get; set; }
        public virtual ICollection<Car> Cars { get; set; }

    }
}
