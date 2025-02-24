using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.MarkDeleted
{
    public record MarkDeletedVolunteerRequest(Guid Id, bool Delete)
    {

    }

}
