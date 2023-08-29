using MediatR;
using System.Text.Json.Serialization;
using TesteGmil.View;

namespace TesteGmil.Model.Commands
{
    public class UpdateGenreCommand : IRequest<ApiResult<GenreDto>>
    {
        public UpdateGenreCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
