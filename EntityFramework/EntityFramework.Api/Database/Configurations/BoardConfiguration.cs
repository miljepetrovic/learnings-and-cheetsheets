using EntityFramework.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFramework.Api.Database.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.ToTable("Boards");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(b => b.Status)
            .HasConversion<string>()
            .IsRequired();
        builder.HasMany<Column>(b => b.Columns)
            .WithOne(c => c.Board)
            .HasForeignKey(c => c.BoardId);
        builder.HasIndex(b => b.UserId);
        //Created index for status field since boards will always be filtered by this active status.
        builder.HasIndex(b => b.Status);
    }
}