using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.MarkDeleted
{
    public class MarkDeletedVolunteerHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public MarkDeletedVolunteerHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> HandleAsync(
            MarkDeletedVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var volunteer = volunteerResult.Value;

            var result = request.Delete ? volunteer.Delete() : volunteer.Restore();

            if (result.IsFailure)
            {
                return result.Error;
            }

            var saveResult = await _volunteersRepository.SaveAsync(volunteer, cancellationToken);
            if (saveResult.IsFailure)
            {
                return saveResult.Error;
            }

            if (request.Delete)
            {
                _logger.LogInformation("soft deleted volunteer with id: {id}", volunteer.Id);
            }
            else
            {
                _logger.LogInformation("restore volunteer with id: {id}", volunteer.Id);
            }
            

            return volunteer.Id;
        }
    }

}
