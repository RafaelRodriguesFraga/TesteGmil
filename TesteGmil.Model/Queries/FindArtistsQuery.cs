using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Queries
{
    public class FindArtistsQuery : IRequest<ApiResult<IEnumerable<ArtistDto>>>
    {
    }
}
