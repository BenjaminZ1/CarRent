using System;

namespace CarRent.Reservation.Domain
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Car.Domain.Car Car { get; set; }


        public double TotalDays()
        {
           return (EndDate - StartDate).TotalDays;
        }

        public decimal TotalFee()
        {
            return Car.Class.PricePerDay * (decimal)TotalDays();
        }
    }
}
