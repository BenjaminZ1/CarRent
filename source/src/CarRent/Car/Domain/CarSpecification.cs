namespace CarRent.Car.Domain
{
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
