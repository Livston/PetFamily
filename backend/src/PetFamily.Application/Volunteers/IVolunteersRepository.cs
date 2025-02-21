using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers
{
    public interface IVolunteersRepository
    {
        Task<Result<Guid, Error>> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<Guid, Error>> SaveAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<Volunteer, Error>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}