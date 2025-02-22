using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Volunteers
{
    public record HelpDetails
    {
        public HelpDetails()
        {
        }

        public HelpDetails(IEnumerable<HelpRequisite> helpRequisites)
        {
            HelpRequisites = helpRequisites.ToList();
        }

        //public IReadOnlyList<HelpRequisite> HelpRequisites { get; } = [];
        public IReadOnlyList<HelpRequisite> HelpRequisites { get; }
    }
}
