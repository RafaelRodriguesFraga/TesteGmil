using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Models;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers.Commands
{
    public class DeleteMusicCommandHandler : IRequestHandler<DeleteMusicCommand, ApiResult<string>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public DeleteMusicCommandHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<string>> Handle(DeleteMusicCommand request, CancellationToken cancellationToken)
        {
            var artistMusic = await _context.ArtistMusic
                .Where(a => a.MusicId == request.Id)
                .FirstOrDefaultAsync();

            if (artistMusic == null)
            {
                return new ApiResult<string>(false, 400, null, "Musica não encontrada");
            }

            var music = await _context.Musics.Where(m => m.Id == artistMusic.MusicId).FirstOrDefaultAsync();           

            _context.Remove(artistMusic);
            _context.Remove(music);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, 200, "Deletado com successo", null);
        }
    }
}
