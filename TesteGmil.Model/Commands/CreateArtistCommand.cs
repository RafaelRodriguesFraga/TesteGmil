using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Commands
{
    public class CreateArtistCommand : IRequest<ApiResult<ArtistDto>>
    {
        public string Name { get; set; }
    }
}
