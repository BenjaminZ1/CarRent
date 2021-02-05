using System.Collections.Generic;
using CarRent.Common.Domain;

namespace CarRent.User.Domain
{
    public class User : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Place { get; set; }
        public string Plz { get; set; }
        public virtual Reservation.Domain.Reservation Reservation { get; set; }
        public virtual ICollection<Reservation.Domain.Reservation> Reservations { get; set; }
    }
}
