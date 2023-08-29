using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Commands
{
    public class CreateGenreCommand : IRequest<ApiResult<GenreDto>>
    {
        public string Name { get; set; }
    }
}
