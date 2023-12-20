using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Example.Data;

internal sealed class ExampleDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Domain.Example> Examples { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder
            .HasDefaultSchema("example")
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
