using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Repositories
{
    public class VolunteersRepository : IVolunteersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VolunteersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Volunteer, Error>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var volunteer = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

            if (volunteer == null)
            {
                return Errors.General.NotFound(id);
            }

            return volunteer;
        }

        public async Task<Result<Guid, Error>> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return volunteer.Id;
        }

        public async Task<Result<Guid, Error>> SaveAsync(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return volunteer.Id;
        }

        public async Task<Result<Guid, Error>> DeleteAsync(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(volunteer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return volunteer.Id;
        }
    }
}
