using CSharpFunctionalExtensions;
using FluentValidation;
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
    public class CreateVolunteerHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;

        public CreateVolunteerHandler(
            IVolunteersRepository volunteersRepository)
        {
            _volunteersRepository = volunteersRepository;

        }

        public async Task<Result<Guid, Error>> HandleAsync(
            CreateVolunteerRequest request, CancellationToken cancellationToken = default)
        {
            var volunteer = Volunteer
                .Create(Guid.NewGuid(), request.Name, request.LastName).Value;

            if (request.HelpRequisiteDTOs.Count() > 0)
            {
                var helpRequisites = request.HelpRequisiteDTOs
                    .Select(x => HelpRequisite.Create(x.Name, x.Description).Value)
                    .ToList();

                volunteer.HelpDetails = new HelpDetails(helpRequisites);
            }

            var addResult = await _volunteersRepository.AddAsync(volunteer, cancellationToken);

            if (addResult.IsFailure)
            {
                return addResult.Error;
            }

            return addResult.Value;
        }
    }
}
