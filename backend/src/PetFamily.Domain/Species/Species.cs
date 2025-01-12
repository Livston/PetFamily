using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PetFamily.Domain.Species
{
    public class Species : Entity<Guid>
    {
        private readonly List<Breed> _breeds = [];

        private Species(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public IReadOnlyList<Breed> Breeds => _breeds;

        public static Result<Species, string> Create(Guid id, string name)
        {
            return new Species(id, name);
        }

        public Result<Breed, string> AddBreed(Breed breed)
        {
            _breeds.Add(breed);
            return breed;
        }

    }
}
