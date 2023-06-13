using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Mapping;

public class PlayerMapping : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> entity)
    {
        entity.ToTable("player");
        
        entity.HasKey(e => e.Id);
        
        entity.Property(e => e.Code)
            .HasColumnOrder(1)
            .HasDefaultValueSql("nextval('\"PlayerSequence\"')");

        entity.Property(e => e.CreatedAt);

        entity.Property(e => e.Name);
        
        entity.Property(e => e.SquadId);
        
        entity.Property(e => e.PlayerTypeId);
        
        entity.Property(e => e.UserId);
        
        entity.Property(e => e.SkillLevel)
            .HasDefaultValue(0);
        
        entity.HasOne(a => a.Squad)
            .WithMany(c => c.Players)
            .HasForeignKey(a => a.SquadId)
            .HasConstraintName("fk_player_squadId");
        
        entity.HasOne(a => a.PlayerType)
            .WithMany(c => c.Players)
            .HasForeignKey(a => a.PlayerTypeId)
            .HasConstraintName("fk_player_playerTypeId");
        
        entity.HasOne(a => a.User)
            .WithMany(c => c.Players)
            .HasForeignKey(a => a.UserId)
            .HasConstraintName("fk_player_userId");
        
    }
}