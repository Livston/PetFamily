using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Volunteers
{
    public record SocialNetworksDetails
    {
        public List<SocialNetwork> SocialNetworks { get; } = [];
    }
}
