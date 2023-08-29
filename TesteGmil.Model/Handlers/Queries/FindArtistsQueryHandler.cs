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
    public class FindArtistsQueryHandler : IRequestHandler<FindArtistsQuery, ApiResult<IEnumerable<ArtistDto>>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public FindArtistsQueryHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<ArtistDto>>> Handle(FindArtistsQuery request, CancellationToken cancellationToken)
        {
            var artists = await  _context
                .Artists
                .ToListAsync();

            var listArtistDto = _mapper.Map<IEnumerable<ArtistDto>>(artists);

            return new ApiResult<IEnumerable<ArtistDto>>(true, 200, listArtistDto, null);
        }
    }
}
