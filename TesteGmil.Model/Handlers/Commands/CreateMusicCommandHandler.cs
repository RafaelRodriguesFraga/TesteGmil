using AutoMapper;
using MediatR;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Models;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers.Commands
{
    public class CreateMusicCommandHandler : IRequestHandler<CreateMusicCommand, ApiResult<MusicDto>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public CreateMusicCommandHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<MusicDto>> Handle(CreateMusicCommand request, CancellationToken cancellationToken)
        {

            var music = new Music(request.Title, request.GenderId);
            var artistMusic = new ArtistMusic(request.ArtistId, music.Id);

            _context.Add(music);
            _context.Add(artistMusic);

            await _context.SaveChangesAsync(cancellationToken);

            var musicDto = _mapper.Map<MusicDto>(music);

            return new ApiResult<MusicDto>(true, 201, musicDto, null);
        }
    }
}
