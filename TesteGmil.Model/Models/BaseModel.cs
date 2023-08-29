namespace TesteGmil.Model.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
    }
}
