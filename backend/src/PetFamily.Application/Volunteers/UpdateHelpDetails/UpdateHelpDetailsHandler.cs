using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateHelpDetails
{
    public class UpdateHelpDetailsHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<UpdateHelpDetailsHandler> _logger;

        public UpdateHelpDetailsHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<UpdateHelpDetailsHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> HandleAsync(
            UpdateHelpDetailsRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var volunteer = volunteerResult.Value;

            var helpRequisites = request.HelpRequisiteDTOs
                .Select(x => HelpRequisite.Create(x.Name, x.Description).Value)
                .ToList();

            volunteer.HelpDetails = new HelpDetails(helpRequisites);

            var updateResult = await _volunteersRepository.SaveAsync(volunteer, cancellationToken);

            _logger.LogInformation("update help requisites: volunteer {name} - {lastName} with id: {id}", volunteer.Name, volunteer.LastName, volunteer.Id);

            return updateResult.Value;
        }
    }
}
