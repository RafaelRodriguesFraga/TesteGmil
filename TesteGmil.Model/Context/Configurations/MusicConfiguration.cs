using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteGmil.Model.Models;

namespace TesteGmil.Model.Context.Configurations
{
    internal class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).HasColumnType("varchar(36)");

            builder
                .Property(m => m.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasOne(m => m.Genre)
                .WithMany(g => g.Musics)
                .HasForeignKey(m => m.GenreId);               

            builder.ToTable("Musics");
        }
    }
}
