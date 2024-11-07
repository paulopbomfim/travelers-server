namespace Travelers.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit(CancellationToken ct);
}