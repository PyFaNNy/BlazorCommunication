using BlazorCommunication.Domain.Interfaces;

namespace BlazorCommunication.Domain.Entities;

public class User : IBaseEntity, IRemovedAt, ICreatedAt, IUpdatedAt
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime? RemovedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}