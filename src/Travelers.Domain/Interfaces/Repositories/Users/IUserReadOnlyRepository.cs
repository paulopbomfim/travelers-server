namespace Travelers.Domain.Interfaces.Repositories;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email, CancellationToken ct);
}