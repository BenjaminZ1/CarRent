using System.Collections.Generic;

namespace CarRent.Car.Domain
{
    public abstract class CarClass
    {
        public int Id { get; set; }
        public decimal PricePerDay { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }

    public class LuxuryCarClass : CarClass
    {
        public LuxuryCarClass(int id, decimal pricePerDay, string description)
        {
            Id = id;
            PricePerDay = pricePerDay;
            Description = description;
        }
    }

    public class MediumCarClass : CarClass
    {
        public MediumCarClass(decimal pricePerDay, string description)
        {
            PricePerDay = pricePerDay;
            Description = description;
        }
    }

    public class EasyCarClass : CarClass
    {
        public EasyCarClass(decimal pricePerDay, string description)
        {
            PricePerDay = pricePerDay;
            Description = description;
        }
    }
}
