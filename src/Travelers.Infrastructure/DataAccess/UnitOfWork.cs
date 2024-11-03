﻿using Travelers.Domain.Repositories;

namespace Travelers.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly TravelersDbContext _dbContext;

    public UnitOfWork(TravelersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}