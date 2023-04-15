using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Mapping;

public class SquadMapping : IEntityTypeConfiguration<Squad>
{
    public void Configure(EntityTypeBuilder<Squad> entity)
    {
        entity.ToTable("squad");
        
        entity.HasKey(e => e.Id);
        
        entity.Property(e => e.Code)
            .HasColumnOrder(1)
            .HasDefaultValueSql("nextval('\"SquadSequence\"')");

        entity.Property(e => e.CreatedAt);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(150);
        
        entity.Property(e => e.UserId);
        
        entity.HasOne(e => e.User)
            .WithMany(c => c.Squads)
            .HasForeignKey(a => a.UserId)
            .HasConstraintName("fk_squad_userId");
    }
}