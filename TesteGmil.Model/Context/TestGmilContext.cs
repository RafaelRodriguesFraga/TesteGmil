using Microsoft.EntityFrameworkCore;
using TesteGmil.Model.Context.Configurations;
using TesteGmil.Model.Models;

namespace TesteGmil.Model.Context
{
    public class TestGmilContext : DbContext
    {
        public TestGmilContext()
        {
                
        }
        public TestGmilContext(DbContextOptions<TestGmilContext> options) : base(options)
        {
                
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ArtistMusic> ArtistMusic { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new MusicConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistMusicConfiguration());
        }      
    }
}
