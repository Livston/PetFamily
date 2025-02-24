using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.Delete
{
    public class DeleteVolunteerHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public DeleteVolunteerHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> HandleAsync(
            DeleteVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var volunteer = volunteerResult.Value;

            var deleteResult = await _volunteersRepository.DeleteAsync(volunteer, cancellationToken);
            if (deleteResult.IsFailure)
            {
                return deleteResult.Error;
            }

            _logger.LogInformation("Deleted volunteer with id: {id}", volunteer.Id);

            return volunteer.Id;
        }
    }

}
