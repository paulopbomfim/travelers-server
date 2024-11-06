using Microsoft.EntityFrameworkCore;
using Travelers.Domain.Entities;

namespace Travelers.Infrastructure.DataAccess;

public class TravelersDbContext : DbContext
{
    public TravelersDbContext(DbContextOptions options) : base(options) {}
    
    public DbSet<User> Users { get; init; }
}