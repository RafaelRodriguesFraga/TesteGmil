using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Queries
{
    public class FindMusicsQuery : IRequest<ApiResult<IEnumerable<MusicDto>>>
    {
    }
}
