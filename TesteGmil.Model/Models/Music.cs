namespace TesteGmil.Model.Models
{
    public class Music : BaseModel
    {
        public Music()
        {
                
        }
        public Music(string title, Guid genreId)
        {
            Title = title;
            GenreId = genreId;           
        }

        public string Title { get; private set; }
        public Guid GenreId { get; private set; }
        public Genre Genre { get; private set; }
        public ICollection<ArtistMusic> ArtistMusics { get; private set; }

        public void Update(string title)
        {
            Title = string.IsNullOrEmpty(title) ? Title : title;
        }

    }
}

