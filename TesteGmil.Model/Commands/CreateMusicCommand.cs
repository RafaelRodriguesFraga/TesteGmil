using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Commands
{
    public class CreateMusicCommand : IRequest<ApiResult<MusicDto>>
    {
        public string Title { get; set; }
        public Guid GenderId { get; set; }
        public Guid ArtistId { get; set; }
    }
}
