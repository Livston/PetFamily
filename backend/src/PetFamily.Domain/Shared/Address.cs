using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteer
{
    public record Address
    {

        private Address(string city, string street, string home, string index)
        {
            City = city;
            Street = street;
            Home = home;
            Index = index;
        }

        public string City { get; }
        public string Street { get; }
        public string Home { get; }
        public string Index { get; }

        public static Result<Address, string> Create(string city, string street, string home, string index)
        {
            //validation
            return new Address(city, street, home, index);
        }
    }
}
