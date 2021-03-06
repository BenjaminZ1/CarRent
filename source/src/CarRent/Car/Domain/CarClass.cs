﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.Car.Domain
{
    [Table("Class")]
    public abstract class CarClass
    {
        public int Id { get; set; }
        public decimal PricePerDay { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual int ReservationRef { get; set; }
        public virtual Reservation.Domain.Reservation Reservation { get; set; }
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
