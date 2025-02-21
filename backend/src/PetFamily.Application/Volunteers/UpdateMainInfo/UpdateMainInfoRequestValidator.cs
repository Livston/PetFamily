using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateMainInfo
{
    public class UpdateMainInfoRequestValidator : AbstractValidator<UpdateMainInfoRequest>
    {
        public UpdateMainInfoRequestValidator()
        {
            RuleFor(r => r.ExperienceInYears).LessThan(Volunteer.MAX_ExperienceInYears)
                .WithError(Errors.General.ValueIsInvalid("ExperienceInYears"));

            RuleFor(r => r.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid("Id"));

            RuleFor(r => r.TelephoneNumber).CreateMethod(TelephoneNumber.Create);

            RuleFor(r => new { r.Name, r.LastName })
                .CreateMethod(x => Volunteer.Create(Guid.NewGuid(), x.Name, x.LastName));
        }
    }
}
