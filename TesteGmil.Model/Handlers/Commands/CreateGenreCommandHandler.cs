using AutoMapper;
using MediatR;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Models;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers.Commands
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, ApiResult<GenreDto>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<GenreDto>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre(request.Name);

            _context.Add(genre);
            await _context.SaveChangesAsync(cancellationToken);

            var genreDto = _mapper.Map<GenreDto>(genre);

            return new ApiResult<GenreDto>(true, 201, genreDto, null);
        }
    }
}
