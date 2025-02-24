using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateMainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public UpdateMainInfoHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> HandleAsync(
            UpdateMainInfoRequest request, 
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
            if (volunteerResult.IsFailure) 
            {
                return volunteerResult.Error;
            }

            var volunteer = volunteerResult.Value;

            TelephoneNumber telephoneNumber = TelephoneNumber.Create(request.TelephoneNumber).Value;

            var updateMainInfoResult = volunteer.UpdateMainInfo(request.Name, request.LastName, telephoneNumber, request.ExperienceInYears);
            if (updateMainInfoResult.IsFailure)
            {
                return updateMainInfoResult.Error;
            }

            var updateResult = await _volunteersRepository.SaveAsync(volunteer, cancellationToken);
            
            _logger.LogInformation("updat main info volunteer {name} - {lastName} with id: {id}", volunteer.Name, volunteer.LastName, volunteer.Id);

            return updateResult.Value;
        }
    }
}
