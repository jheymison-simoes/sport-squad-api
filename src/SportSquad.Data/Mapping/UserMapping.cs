using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Mapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("user");
        
        entity.HasKey(e => e.Id);
        
        entity.Property(e => e.Code)
            .HasColumnOrder(1)
            .HasDefaultValueSql("nextval('\"UserSequence\"')");

        entity.Property(e => e.CreatedAt);

        entity.Property(e => e.Ddd)
            .IsRequired()
            .HasMaxLength(2);
        
        entity.Property(e => e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(9);
        
        entity.Property(e => e.Email)
            .HasMaxLength(250);
        
        entity.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(1024);
    }
}