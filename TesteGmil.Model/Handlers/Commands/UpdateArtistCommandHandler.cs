using AutoMapper;
using MediatR;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Models;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers.Commands
{
    public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, ApiResult<ArtistDto>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public UpdateArtistCommandHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<ArtistDto>> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
        {
            var artist = _context.Artists.Where(a => a.Id == request.Id).FirstOrDefault();
            if (artist == null)
            {
                return new ApiResult<ArtistDto>(false, 400, null, "Artista não encontrado");
            }

            artist.Update(request.Name);

            _context.Update(artist);
            await _context.SaveChangesAsync();

            var artistDto = _mapper.Map<ArtistDto>(artist);

            return new ApiResult<ArtistDto>(true, 200, artistDto, null);
        }
    }
}
