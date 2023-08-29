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
    public class FindGenresQueryHandler : IRequestHandler<FindGenresQuery, ApiResult<IEnumerable<GenreDto>>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public FindGenresQueryHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<GenreDto>>> Handle(FindGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await  _context
                .Genres
                .ToListAsync();

            var listGenreDto = _mapper.Map<IEnumerable<GenreDto>>(genres);

            return new ApiResult<IEnumerable<GenreDto>>(true, 200, listGenreDto, null);
        }
    }
}
