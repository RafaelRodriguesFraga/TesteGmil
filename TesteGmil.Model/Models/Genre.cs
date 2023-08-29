namespace TesteGmil.Model.Models
{
    public class Genre : BaseModel
    {
        public Genre()
        {

        }
        public Genre(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public ICollection<Music> Musics { get; set; }

        public void Update(string name)
        {
            Name = string.IsNullOrEmpty(name) ? Name : name;
        }
    }
}
