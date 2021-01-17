using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CarRent.Car.Domain
{
    public interface ICarClassFactory
    {
        CarClass GetCarClass(int? id);
    }
    public class CarClassFactory : ICarClassFactory
    {
        public CarClass GetCarClass(int? id)
        {
            return id switch
            {
                1 => new LuxuryCarClass(1,249.99m, "Mehr Komfort und Luxus geht nicht."),
                2 => new MediumCarClass(149.99m, "Die beste Wahl für den Alltag."),
                3 => new EasyCarClass(69.99m, "Die beste Wahl für den sparsamen Fahrer."),
                _ => null
            };
        }
    }
}
