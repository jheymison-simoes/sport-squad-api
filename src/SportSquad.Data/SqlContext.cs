using SportSquad.Domain.Models;
using Marques.EFCore.SnakeCase;
using Microsoft.EntityFrameworkCore;
using SportSquad.Core.Interfaces;

namespace SportSquad.Data;

public class SqlContext : DbContext, IUnitOfWork
{
    public SqlContext(DbContextOptions<SqlContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Squad> Squads { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<SquadConfig> SquadConfigs { get; set; }
    public DbSet<PlayerType> PlayerTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlContext).Assembly);
        modelBuilder.ToSnakeCase();
        
        modelBuilder.HasSequence<long>("UserSequence").StartsAt(1).IncrementsBy(1);
        modelBuilder.HasSequence<long>("PlayerSequence").StartsAt(1).IncrementsBy(1);
        modelBuilder.HasSequence<long>("PlayerTypeSequence").StartsAt(1).IncrementsBy(1);
        modelBuilder.HasSequence<long>("SquadSequence").StartsAt(1).IncrementsBy(1);
        modelBuilder.HasSequence<long>("SquadConfigSequence").StartsAt(1).IncrementsBy(1);
        
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        var context = this;
        var rowsEffected = await context.SaveChangesAsync();
        return rowsEffected > default(int);
    }
}