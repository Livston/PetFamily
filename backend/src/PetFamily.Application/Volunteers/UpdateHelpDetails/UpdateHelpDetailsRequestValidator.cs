using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateHelpDetails
{
    public class UpdateHelpDetailsRequestValidator : AbstractValidator<UpdateHelpDetailsRequest>
    {
        public UpdateHelpDetailsRequestValidator()
        {
            RuleFor(r => r.HelpRequisiteDTOs).Must(r => r.Any())
                .WithError(Errors.General.ValueIsRequired("Help requisite"));
            
            RuleForEach(r => r.HelpRequisiteDTOs)
                .CreateMethod(r => HelpRequisite.Create(r.Name, r.Description));
        }
    }
}
