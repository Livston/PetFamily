using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Domain.Volunteer
{
    public class Volunteer : Entity<Guid>
    {
        private readonly List<Pet> _pets = [];

        private Volunteer(Guid id, string name, string lastName, string secondName) : base(id)
        {
            Name = name;
            LastName = lastName;
            SecondName = secondName;
        }

        private Volunteer(Guid id, string name, string lastName) : base(id)
        {
            Name = name;
            LastName = lastName;
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string SecondName { get; private set; } = string.Empty;
        public string FIO => (Name + " " + LastName + " " + SecondName).TrimEnd();
        public string Description { get; private set; } = string.Empty;
        public int ExperienceInYears { get; private set; }
        public TelephoneNumber? TelephoneNumber { get; private set; }
        public int PetsFindedHomeCount => _pets.Where(p => p.HelpStatus == PetsHelpStatus.FindedHome).Count();
        public int PetsNeedHomeCount => _pets.Where(p => p.HelpStatus == PetsHelpStatus.NeedHome).Count();
        public int PetsNeedHelpCount => _pets.Where(p => p.HelpStatus == PetsHelpStatus.NeedHelp).Count();
        public SocialNetworksDetails? socialNetworksDetails { get; private set; }
        public HelpDetails? HelpDetails { get; set; }
        public IReadOnlyList<Pet> Pets => _pets;

        public static Result<Volunteer, string> Create(Guid id, string name, string lastName)
        {
            if (id == Guid.Empty)
            {
                return "invalid id";
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length > Constans.MAX_NAMES_LENGH)
            {
                return "invalid name";
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Constans.MAX_NAMES_LENGH)
            {
                return "invalid lastName";
            }

            return new Volunteer(id, name, lastName);
        }
        public static Result<Volunteer, string> Create(Guid id, string name, string lastName, string secondName)
        {
            if (id == Guid.Empty)
            {
                return "invalid id";
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length > Constans.MAX_NAMES_LENGH)
            {
                return "invalid name";
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Constans.MAX_NAMES_LENGH)
            {
                return "invalid lastName";
            }

            if (string.IsNullOrWhiteSpace(secondName) || secondName.Length > Constans.MAX_NAMES_LENGH)
            {
                return "invalid secondName";
            }

            return new Volunteer(id, name, lastName, secondName);

        }

        public Result<Pet, string> AddPet(Pet pet)
        {
            if (pet.HelpStatus == PetsHelpStatus.FindedHome)
            {
                return "Pet has home";
            }
            _pets.Add(pet);

            return pet;
        }
    }
}
