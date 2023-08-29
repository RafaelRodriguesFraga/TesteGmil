using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Queries
{
    public class FindGenresQuery : IRequest<ApiResult<IEnumerable<GenreDto>>>
    {
    }
}
