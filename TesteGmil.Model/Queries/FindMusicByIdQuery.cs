using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Queries
{
    public class FindMusicByIdQuery : IRequest<ApiResult<MusicDto>>
    {
        public FindMusicByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
