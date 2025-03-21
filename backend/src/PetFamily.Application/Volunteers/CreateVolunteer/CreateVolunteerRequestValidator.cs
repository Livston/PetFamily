﻿using FluentValidation;
using FluentValidation.AspNetCore;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.CreateVolunteer 
{
    public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
    {
        public CreateVolunteerRequestValidator() 
        {
            //оставил для примера валидации обычных полей
            //RuleFor(c => c.Age).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("Age"));
            
            RuleFor(c => new { c.Name, c.LastName })
                .CreateMethod(x => Volunteer.Create(Guid.NewGuid(), x.Name, x.LastName));

            RuleForEach(c => c.HelpRequisiteDTOs)
                .CreateMethod(r => HelpRequisite.Create(r.Name, r.Description));
        }
    }
}
