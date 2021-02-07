using CarRent.Car.Application;
using CarRent.User.Application;
using System;

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
        public UserDto User { get; set; }

        public ReservationDto() { }

        public ReservationDto(Domain.Reservation reservation)
        {
            this.Class ??= new CarClassDto();
            this.User ??= new UserDto();

            Id = reservation.Id;
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
            TotalDays = reservation.TotalDays();
            TotalFee = reservation.TotalFee();
            if (reservation.Class != null)
            {
                Class.Description = reservation.Class.Description;
                Class.PricePerDay = reservation.Class.PricePerDay;
            }

            if (reservation.User != null)
            {
                User.Name = reservation.User.Name;
                User.LastName = reservation.User.LastName;
                User.Place = reservation.User.Place;
                User.Street = reservation.User.Street;
                User.Plz = reservation.User.Plz;
            }
        }
    }
}
