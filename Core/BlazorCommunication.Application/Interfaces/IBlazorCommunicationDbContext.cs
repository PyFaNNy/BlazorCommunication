using Microsoft.EntityFrameworkCore;
using BlazorCommunication.Domain.Entities;

namespace BlazorCommunication.Application.Interfaces;

public interface IBlazorCommunicationDbContext
{
    DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}