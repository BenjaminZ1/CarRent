using CarRent.Common.Domain;
using System;

namespace CarRent.Reservation.Domain
{
    public class Reservation : EntityBase
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Car.Domain.CarClass Class { get; set; }
        public virtual int? ClassRef { get; set; }
        public virtual User.Domain.User User { get; set; }
        public virtual int? UserRef { get; set; }


        public double TotalDays()
        {
            return (EndDate - StartDate).TotalDays;
        }

        public decimal TotalFee()
        {
            return Class.PricePerDay * (decimal)TotalDays();
        }
    }
}
