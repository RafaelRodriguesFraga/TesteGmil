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
    public class FindMusicsQueryHandler : IRequestHandler<FindMusicsQuery, ApiResult<IEnumerable<MusicDto>>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public FindMusicsQueryHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<MusicDto>>> Handle(FindMusicsQuery request, CancellationToken cancellationToken)
        {
            var musics = await  _context
                .Musics
                .Include(g => g.Genre)
                .ToListAsync();

            var listMusicDto = _mapper.Map<IEnumerable<MusicDto>>(musics);

            return new ApiResult<IEnumerable<MusicDto>>(true, 200, listMusicDto, null);
        }
    }
}
