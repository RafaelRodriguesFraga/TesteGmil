using MediatR;
using System.Text.Json.Serialization;
using TesteGmil.View;

namespace TesteGmil.Model.Commands
{
    public class UpdateArtistCommand : IRequest<ApiResult<ArtistDto>>
    {
        public UpdateArtistCommand()
        {
            
        }
        public UpdateArtistCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
