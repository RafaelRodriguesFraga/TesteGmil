using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Queries
{
    public class FindGenreByIdQuery : IRequest<ApiResult<GenreDto>>
    {
        public FindGenreByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
