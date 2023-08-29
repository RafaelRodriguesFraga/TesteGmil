using AutoMapper;
using MediatR;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Models;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers.Commands
{
    public class UpdateMusicCommandHandler : IRequestHandler<UpdateMusicCommand, ApiResult<MusicDto>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public UpdateMusicCommandHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<MusicDto>> Handle(UpdateMusicCommand request, CancellationToken cancellationToken)
        {
            var music = _context.Musics.Where(a => a.Id == request.Id).FirstOrDefault();
            if (music == null)
            {
                return new ApiResult<MusicDto>(false, 400, null, "Música não encontrada");
            }

            music.Update(request.Title);

            _context.Update(music);
            await _context.SaveChangesAsync();

            var musicDto = _mapper.Map<MusicDto>(music);

            return new ApiResult<MusicDto>(true, 200, musicDto, null);
        }
    }
}
