using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Models;
using TesteGmil.Model.Queries;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers
{
    public class FindMusicByIdQueryHandler : IRequestHandler<FindMusicByIdQuery, ApiResult<MusicDto>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public FindMusicByIdQueryHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<MusicDto>> Handle(FindMusicByIdQuery request, CancellationToken cancellationToken)
        {
            var music = await _context
                .Musics
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if (music == null)
            {
                return new ApiResult<MusicDto>(false, 400, null, "Música não encontrada");
            }

            var musicDto = _mapper.Map<MusicDto>(music);

            return new ApiResult<MusicDto>(true, 200, musicDto, null);
        }
    }
}
