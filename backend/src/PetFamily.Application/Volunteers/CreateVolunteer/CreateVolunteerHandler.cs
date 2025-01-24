using CSharpFunctionalExtensions;
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

        public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
        {
            _volunteersRepository = volunteersRepository;
        }

        public async Task<Result<Guid, Error>> HandleAsync(
            CreateVolunteerRequest request, CancellationToken cancellationToken = default)
        {
            var volunteerResult = Volunteer.Create(Guid.NewGuid(), request.Name, request.LastName);

            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var volunteer = volunteerResult.Value;

            if (request.HelpRequisiteDTOs.Count() > 0)
            {
                var helpDetailsResults = request.HelpRequisiteDTOs
                    .Select(x => HelpRequisite.Create(x.Name, x.Description))
                    .ToList();

                if (helpDetailsResults.Any(h => h.IsFailure))
                {
                    return helpDetailsResults.First(h => h.IsFailure).Error;
                };

                volunteer.HelpDetails = new HelpDetails(helpDetailsResults.Select(h => h.Value).ToList());
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
