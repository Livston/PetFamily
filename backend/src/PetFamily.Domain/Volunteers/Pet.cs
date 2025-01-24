using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Domain.Volunteer
{
    public class Pet : Entity<Guid>
    {
        public const int MAX_COLOR_LENGH = 200;

        private Pet(Guid id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string HealthDescription { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public double Weigth { get; private set; }
        public double Height { get; private set; }
        public TelephoneNumber? OwnerTelephonNumber { get; private set; }
        public Address? Adress { get; private set; }
        public bool Neutered { get; private set; }
        public bool Vaccinated { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public DateTime CreationAt { get; private set; } = DateTime.Now;
        public PetsHelpStatus HelpStatus { get; private set; }
        public HelpDetails? HelpDetails { get; private set; }
        public SpeciesBreed? SpeciesBreed { get; private set; }

        public static Result<Pet, Error> Create(Guid id, string name, string description)
        {
            if (id == Guid.Empty)
            {
                return Errors.General.ValueIsRequired("id");
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length > Constans.MAX_NAMES_LENGH)
            {
                return Errors.General.ValueIsInvalid("name");
            }

            if (string.IsNullOrWhiteSpace(description) || description.Length > Constans.MAX_DESCRIPTIONS_LENGH)
            {
                return Errors.General.ValueIsInvalid("description");
            }

            return new Pet(id, name, description);
        }

    }
}
