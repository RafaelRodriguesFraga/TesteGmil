using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Models;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers.Commands
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, ApiResult<string>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public DeleteGenreCommandHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<string>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _context.Genres.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (genre == null)
            {
                return new ApiResult<string>(false, 400, null, "Genero não encontrado");
            }

            _context.Remove(genre);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, 200, "Deletado com successo", null);

        }
    }
}
