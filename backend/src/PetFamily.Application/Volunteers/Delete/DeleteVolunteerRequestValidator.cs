﻿using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.Delete
{
    public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerRequest>
    {
        public DeleteVolunteerRequestValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid("Id"));
        }
    }
}
