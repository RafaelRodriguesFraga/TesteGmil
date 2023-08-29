using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TesteGmil.Model.Models;

namespace TesteGmil.Model.Context.Configurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).HasColumnType("varchar(36)");

            builder
                .Property(s => s.Name)
                .IsRequired()
            .HasMaxLength(30);

            builder.ToTable("Artists");
        }
    }
}
