using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species
{
    public class Breed : Entity<Guid>
    {
        private Breed(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public static Result<Breed, string> Create(Guid id, string name)
        {
            return new Breed(id, name);
        }

    }
}
