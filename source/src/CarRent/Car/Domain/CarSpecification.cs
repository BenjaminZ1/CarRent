using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.Car.Domain
{
    [Table("Specification")]
    public class CarSpecification
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int EngineDisplacement { get; set; }
        public int EnginePower { get; set; }
        public virtual int CarRef { get; set; }
        public virtual Car Car { get; set; }

    }
}
