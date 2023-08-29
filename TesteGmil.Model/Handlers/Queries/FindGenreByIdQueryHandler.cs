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
    public class FindGenreByIdQueryHandler : IRequestHandler<FindGenreByIdQuery, ApiResult<GenreDto>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public FindGenreByIdQueryHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<GenreDto>> Handle(FindGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await  _context
                .Genres
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if(genre == null)
            {
                return new ApiResult<GenreDto>(false, 400, null, "Genero não encontrado");
            }

            var genreDto = _mapper.Map<GenreDto>(genre);

            return new ApiResult<GenreDto>(true, 200, genreDto, null);
        }
    }
}
