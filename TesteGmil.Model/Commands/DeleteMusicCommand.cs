using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Commands
{
    public class DeleteMusicCommand : IRequest<ApiResult<string>>
    {
        public DeleteMusicCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
