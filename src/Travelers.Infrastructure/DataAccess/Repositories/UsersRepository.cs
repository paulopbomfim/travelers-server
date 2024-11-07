using Microsoft.EntityFrameworkCore;
using Travelers.Domain.Entities;
using Travelers.Domain.Interfaces.Repositories;

namespace Travelers.Infrastructure.DataAccess.Repositories;

public class UsersRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly TravelersDbContext _dbContext;

    public UsersRepository(TravelersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async  Task Add(User user, CancellationToken ct)
    {
        await _dbContext.Users.AddAsync(user, ct);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email, CancellationToken ct)
    {
        return await _dbContext.Users.AnyAsync(u => u.TxEmail.Equals(email), ct);
    }
}