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
        public CarClassDto Class { get; set; }

        public CarDto(Domain.Car x)
        {
           
            this.Specification ??= new CarSpecificationDto();
            this.Class ??= new CarClassDto();

            Id = x.Id;
            Brand = x.Brand;
            Model = x.Model;
            Type = x.Type;
            Specification.Year = x.Specification.Year;
            Specification.EngineDisplacement = x.Specification.EngineDisplacement;
            Specification.EnginePower = x.Specification.EnginePower;
            Specification.CarSpecificationId = x.Specification.Id;
            Class.Description = x.Class.Description;
            Class.PricePerDay = x.Class.PricePerDay;
        }
    }
}
