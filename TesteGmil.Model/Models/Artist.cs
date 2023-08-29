namespace TesteGmil.Model.Models
{
    public class Artist : BaseModel
    {
        public Artist() : base () { }

        public Artist(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public ICollection<ArtistMusic> ArtistMusics { get; private set; }

        public void Update(string name)
        {
            Name = string.IsNullOrEmpty(name) ? Name : name;
        }
    }
}
