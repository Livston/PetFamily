using CSharpFunctionalExtensions;
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

        public static Result<HelpRequisite, string> Create(string name, string description)
        {
            //validation
            return new HelpRequisite(name, description);
        }
    }
}
