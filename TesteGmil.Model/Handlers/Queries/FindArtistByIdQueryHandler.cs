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
    public class FindArtistByIdQueryHandler : IRequestHandler<FindArtistByIdQuery, ApiResult<ArtistDto>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public FindArtistByIdQueryHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<ArtistDto>> Handle(FindArtistByIdQuery request, CancellationToken cancellationToken)
        {
            var artist = await  _context
                .Artists
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if(artist == null)
            {
                return new ApiResult<ArtistDto>(false, 400, null, "Artista não encontrado");
            }

            var artistDto = _mapper.Map<ArtistDto>(artist);

            return new ApiResult<ArtistDto>(true, 200, artistDto, null);
        }
    }
}
