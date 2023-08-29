using MediatR;
using TesteGmil.View;

namespace TesteGmil.Model.Commands
{
    public class DeleteArtistCommand : IRequest<ApiResult<string>>
    {
        public DeleteArtistCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
