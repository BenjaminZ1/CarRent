namespace CarRent.User.Application
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Place { get; set; }
        public string Plz { get; set; }

        public UserDto(Domain.User user)
        {
            Id = user.Id;
            Name = user.Name;
            LastName = user.LastName;
            Street = user.Street;
            Place = user.Place;
            Plz = user.Plz;
        }
    }
}
