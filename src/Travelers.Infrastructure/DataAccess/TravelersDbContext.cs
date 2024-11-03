using Microsoft.EntityFrameworkCore;

namespace Travelers.Infrastructure.DataAccess;

public class TravelersDbContext : DbContext
{
    public TravelersDbContext(DbContextOptions options) : base(options)
    {}
}