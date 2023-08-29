using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers.Commands
{
    public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, ApiResult<string>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public DeleteArtistCommandHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<string>> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
        {
            var artistMusic = await _context.ArtistMusic.Where(a => a.ArtistId == request.Id).FirstOrDefaultAsync();

            if (artistMusic == null)
            {
                return new ApiResult<string>(false, 400, null, "Artista não encontrado");
            }

            var artist = await _context.Artists.Where(artist => artist.Id == artistMusic.ArtistId).FirstOrDefaultAsync();

            _context.Remove(artistMusic);
            _context.Remove(artist);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, 200, "Deletado com successo", null);
        }
    }
}
