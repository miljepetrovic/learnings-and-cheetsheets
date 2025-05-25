using EntityFramework.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFramework.Api.Database.Configurations;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.ToTable("Columns");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(b => b.Status)
            .HasConversion<string>()
            .IsRequired();
        builder.HasMany<Card>(c => c.Cards)
            .WithOne(c => c.Column)
            .HasForeignKey(fk => fk.ColumnId);
        builder.HasIndex(c => c.BoardId);
    }
}