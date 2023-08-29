using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Queries
{
    public class FindArtistByIdQuery : IRequest<ApiResult<ArtistDto>>
    {
        public FindArtistByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
