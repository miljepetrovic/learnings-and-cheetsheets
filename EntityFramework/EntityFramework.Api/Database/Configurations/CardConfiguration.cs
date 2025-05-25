using EntityFramework.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFramework.Api.Database.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Cards");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(c => c.Description)
            .HasMaxLength(255);
        builder.Property(b => b.Status)
            .HasConversion<string>()
            .IsRequired();
        builder.HasIndex(c => c.ColumnId);
        //Created index for card name to improve performance for cards search by name
        builder.HasIndex(c => c.Name);
    }
}