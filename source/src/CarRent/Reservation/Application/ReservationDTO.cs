using System;
using CarRent.Car.Application;
using CarRent.Car.Domain;

namespace CarRent.Reservation.Application
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalDays { get; set; }
        public decimal TotalFee { get; set; }
        public CarClassDto Class { get; set; }
        public User.Domain.User User { get; set; }

        public ReservationDto(Domain.Reservation reservation)
        {
            this.Class ??= new CarClassDto();
            this.User ??= new User.Domain.User();

            Id = reservation.Id;
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
            TotalDays = reservation.TotalDays();
            TotalFee = reservation.TotalFee();
            Class.Description = reservation.Class.Description;
            Class.PricePerDay = reservation.Class.PricePerDay;
            User.Name = reservation.User.Name;
            User.LastName = reservation.User.LastName;
        }
    }
}
