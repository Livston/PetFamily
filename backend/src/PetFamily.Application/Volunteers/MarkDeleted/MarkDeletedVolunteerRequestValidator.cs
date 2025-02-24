using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.MarkDeleted
{
    public class MarkDeletedVolunteerRequestValidator : AbstractValidator<MarkDeletedVolunteerRequest>
    {
        public MarkDeletedVolunteerRequestValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid("Id"));
        }
    }
}
