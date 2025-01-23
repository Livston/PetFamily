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

        public static Result<Volunteer, Error> Create(Guid id, string name, string lastName)
        {
            if (id == Guid.Empty)
            {
                return Errors.General.ValueIsRequired("id");
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length > Constans.MAX_NAMES_LENGH)
            {
                return Errors.General.ValueIsInvalid("name");
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Constans.MAX_NAMES_LENGH)
            {
                return Errors.General.ValueIsInvalid("lastName");
            }

            return new Volunteer(id, name, lastName);
        }
        public static Result<Volunteer, Error> Create(Guid id, string name, string lastName, string secondName)
        {
            if (id == Guid.Empty)
            {
                return Errors.General.ValueIsRequired("id");
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length > Constans.MAX_NAMES_LENGH)
            {
                return Errors.General.ValueIsInvalid("name");
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Constans.MAX_NAMES_LENGH)
            {
                return Errors.General.ValueIsInvalid("lastName");
            }

            if (string.IsNullOrWhiteSpace(secondName) || secondName.Length > Constans.MAX_NAMES_LENGH)
            {
                return Errors.General.ValueIsInvalid("secondName");
            }

            return new Volunteer(id, name, lastName, secondName);

        }

        public Result<IReadOnlyList<Pet>, Error> AddPets(IEnumerable<Pet> pets)
        {
            if (pets.Any(p => p.HelpStatus == PetsHelpStatus.FindedHome)) 
            {
                return Errors.General.ValueIsInvalid();
            };
            
            _pets.AddRange(pets);

            return _pets;
        }
    }
}
