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

        public ReservationDto(Domain.Reservation reservation)
        {
            Id = reservation.Id;
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
            TotalDays = reservation.TotalDays();
            TotalFee = reservation.TotalFee();
        }
    }
}
