using AutoMapper;
using MediatR;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Models;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers.Commands
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, ApiResult<GenreDto>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<GenreDto>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = _context.Genres.Where(a => a.Id == request.Id).FirstOrDefault();
            if (genre == null)
            {
                return new ApiResult<GenreDto>(false, 400, null, "Genero não encontrado");
            }

            genre.Update(request.Name);

            _context.Update(genre);
            await _context.SaveChangesAsync();

            var genreDto = _mapper.Map<GenreDto>(genre);

            return new ApiResult<GenreDto>(true, 200, genreDto, null );
        }
    }
}
