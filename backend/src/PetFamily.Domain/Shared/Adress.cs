using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteer
{
    public record Adress
    {
        private Adress(string city, string street, string home, string index)
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

        public static Result<Adress, string>Create(string city, string street, string home, string index)
        {
            //validation
            return new Adress(city, street, home, index);
        }
    }
}
