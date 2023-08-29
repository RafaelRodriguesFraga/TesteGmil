namespace TesteGmil.Model.Models
{
    public class ArtistMusic
    {
        public ArtistMusic()
        {
            
        }

      
        public ArtistMusic(Guid artistId, Guid musicId)
        {
            ArtistId = artistId;
            MusicId = musicId;
        }

        public Guid ArtistId { get; private set; }
        public Artist Artist { get; private set; }
        public Guid MusicId { get; private set; }
        public Music Music { get; private set; }
    }
}
