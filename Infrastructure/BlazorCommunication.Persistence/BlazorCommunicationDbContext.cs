using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BlazorCommunication.Application.Interfaces;
using BlazorCommunication.Domain.Entities;
using BlazorCommunication.Domain.Interfaces;

namespace BlazorCommunication.Persistence;

public class BlazorCommunicationDbContext : DbContext, IBlazorCommunicationDbContext
{
    public DbSet<User> Users { get; set; }
    
    public BlazorCommunicationDbContext(DbContextOptions<BlazorCommunicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
    
    public override int SaveChanges()
    {
        SetCreatedAt();
        SetUpdatedAt();
        SetDeleteAt();
        var result = base.SaveChanges();

        return result;
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetCreatedAt();
        SetUpdatedAt();
        SetDeleteAt();
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
    
    private void SetCreatedAt()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(p => p.State == EntityState.Added))
        {
            if (entry.Entity is ICreatedAt ent)
            {
                ent.CreatedAt = DateTime.UtcNow;
            }
        }
    }
    
    private void SetUpdatedAt()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(p => p.State == EntityState.Modified))
        {
            if (entry.Entity is IUpdatedAt ent)
            {
                ent.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
    
    private void SetDeleteAt()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(p => p.State == EntityState.Deleted))
        {
            if (entry.Entity is IRemovedAt ent)
            {
                ent.RemovedAt = DateTime.UtcNow;
                entry.State = EntityState.Modified;
            }
        }
    }
}