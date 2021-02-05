using CarRent.Common.Domain;

namespace CarRent.Car.Domain
{
    public class Car : EntityBase
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int ClassId { get; set; }

        public virtual CarSpecification Specification { get; set; }

        public virtual int ClassRef { get; set; }

        public virtual CarClass Class { get; set; }
    }
}
