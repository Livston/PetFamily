using PetFamily.Application.Volunteers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateHelpDetails
{
    public record UpdateHelpDetailsRequest(Guid Id, IEnumerable<HelpRequisiteDTO> HelpRequisiteDTOs)
    {
    }
}
