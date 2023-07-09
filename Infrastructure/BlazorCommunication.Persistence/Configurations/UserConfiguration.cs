using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlazorCommunication.Domain.Entities;
using BlazorCommunication.Persistence.Extensions;

namespace BlazorCommunication.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.MapTable("Users", "user");

        builder.Property(x => x.Email)
            .HasMediumMaxLength()
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMediumMaxLength()
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMediumMaxLength()
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .IsRequired();
        
        builder.Property(x => x.PasswordSalt)
            .IsRequired();
    }
}