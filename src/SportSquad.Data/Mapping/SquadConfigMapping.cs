using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Mapping;

public class SquadConfigMapping : IEntityTypeConfiguration<SquadConfig>
{
    public void Configure(EntityTypeBuilder<SquadConfig> entity)
    {
        entity.ToTable("squad_config");
        
        entity.HasKey(e => e.Id);
        
        entity.Property(e => e.Code)
            .HasColumnOrder(1)
            .HasDefaultValueSql("nextval('\"SquadConfigSequence\"')");

        entity.Property(e => e.CreatedAt);

        entity.Property(e => e.QuantityPlayers);

        entity.Property(e => e.AllowSubstitutes);
        
        entity.Property(e => e.PlayerTypeId);

        entity.Property(e => e.SquadId);
        
        entity.HasOne(a => a.PlayerType)
            .WithMany(c => c.SquadConfigs)
            .HasForeignKey(a => a.PlayerTypeId)
            .HasConstraintName("fk_squadConfig_playerTypeId");
        
        entity.HasOne(a => a.Squad)
            .WithMany(c => c.SquadConfigs)
            .HasForeignKey(a => a.SquadId)
            .HasConstraintName("fk_squadConfig_userId");
    }
}