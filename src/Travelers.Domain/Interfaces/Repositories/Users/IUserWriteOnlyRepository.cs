using Travelers.Domain.Entities;

namespace Travelers.Domain.Interfaces.Repositories;

public interface IUserWriteOnlyRepository
{
    Task Add(User user, CancellationToken ct);
}