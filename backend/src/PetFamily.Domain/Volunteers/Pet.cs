using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetFamily.Domain.Volunteer
{
    public class Pet : Entity<Guid>
    {
        const int MAX_NAME_LENGH = 100;
        const int MAX_DESCRIPTION_LENGH = 2000;
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
        public Adress? Adress { get; private set; }
        public bool Neutered { get; private set; }
        public bool Vaccinated { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public DateTime CreationAt { get; private set; } = DateTime.Now;
        public PetsHelpStatus HelpStatus { get; private set; }
        public HelpDetails? HelpDetails { get; private set; }
        public SpeciesBreed? SpeciesBreed { get; private set; }

        public static Result<Pet, string> Create(Guid id, string name, string description)
        {
            if (id == Guid.Empty)
            {
                return "Guid is invalid";
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGH) 
            {
                return "Name is invalid";
            }

            if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_DESCRIPTION_LENGH)
            {
                return "Description is invalid";
            }

            return new Pet(id, name, description);
        }

    }
}
