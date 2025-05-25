using EntityFramework.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFramework.Api.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(u => u.EmailAddress)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasMany<Board>(u => u.Boards)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);
        builder.HasIndex(u => u.EmailAddress);
        builder.HasIndex(u => u.Name);
    }
}