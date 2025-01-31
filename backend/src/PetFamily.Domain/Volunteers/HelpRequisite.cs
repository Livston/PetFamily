using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Volunteers
{
    public record HelpRequisite
    {
        private HelpRequisite(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }

        public static Result<HelpRequisite, Error> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > Constans.MAX_NAMES_LENGH)
            {
                return Errors.General.ValueIsInvalid("name");
            }

            if (string.IsNullOrWhiteSpace(description) || description.Length > Constans.MAX_DESCRIPTIONS_LENGH)
            {
                return Errors.General.ValueIsInvalid("description");
            }

            return new HelpRequisite(name, description);
        }
    }
}
