using PetFamily.Application.Volunteers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerRequest(string Name, string LastName, IEnumerable<HelpRequisiteDTO> HelpRequisiteDTOs);

}
