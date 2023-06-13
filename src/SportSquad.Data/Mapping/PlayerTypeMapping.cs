using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Mapping;

public class PlayerTypeMapping : IEntityTypeConfiguration<PlayerType>
{
    public void Configure(EntityTypeBuilder<PlayerType> entity)
    {
        entity.ToTable("player_type");
        
        entity.HasKey(e => e.Id);
        
        entity.Property(e => e.Code)
            .HasColumnOrder(1)
            .HasDefaultValueSql("nextval('\"PlayerTypeSequence\"')");

        entity.Property(e => e.CreatedAt);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        entity.Property(e => e.Icon)
            .IsRequired()
            .HasDefaultValue("fa-check")
            .HasMaxLength(100);
    }
}