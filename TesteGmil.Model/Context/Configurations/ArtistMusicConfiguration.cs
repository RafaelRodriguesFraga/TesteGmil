using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TesteGmil.Model.Models;

namespace TesteGmil.Model.Context.Configurations
{
    public class ArtistMusicConfiguration : IEntityTypeConfiguration<ArtistMusic>
    {
        public void Configure(EntityTypeBuilder<ArtistMusic> builder)
        {
            builder.HasNoKey();

            builder.HasKey(am => new { am.ArtistId, am.MusicId });

            builder
                .HasOne(am => am.Artist)
                .WithMany(a => a.ArtistMusics)
                .HasForeignKey(am => am.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(am => am.Music)
                .WithMany(m => m.ArtistMusics)
                .HasForeignKey(am => am.MusicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
