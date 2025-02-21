using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateMainInfo
{
    public record UpdateMainInfoRequest(Guid Id, string Name, string LastName, string TelephoneNumber, int ExperienceInYears) 
    {
        public static UpdateMainInfoRequest UpdateMainInfoRequestFromDto(Guid id, UpdateMainInfoDto dto)
        {
            return new UpdateMainInfoRequest(id, dto.Name, dto.LastName, dto.TelephoneNumber, dto.ExperienceInYears);
        }
    }
    ;
    public record UpdateMainInfoDto(string Name, string LastName, string TelephoneNumber, int ExperienceInYears);
    



}
