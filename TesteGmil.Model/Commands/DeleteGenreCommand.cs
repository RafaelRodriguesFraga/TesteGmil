using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Commands
{
    public class DeleteGenreCommand : IRequest<ApiResult<string>>
    {
        public DeleteGenreCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
