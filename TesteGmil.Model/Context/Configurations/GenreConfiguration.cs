using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteGmil.Model.Models;

namespace TesteGmil.Model.Context.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(s => s.Id);
            
            builder.Property(s => s.Id).HasColumnType("varchar(36)");

            builder
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.ToTable("Genres");
        }
    }
}
