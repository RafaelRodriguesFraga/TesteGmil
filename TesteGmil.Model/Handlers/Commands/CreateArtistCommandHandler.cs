using AutoMapper;
using MediatR;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Models;
using TesteGmil.View;

namespace TesteGmil.Model.Handlers.Commands
{
    public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, ApiResult<ArtistDto>>
    {
        private readonly TestGmilContext _context;
        private readonly IMapper _mapper;

        public CreateArtistCommandHandler(TestGmilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<ArtistDto>> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
        {
            var artist = new Artist(request.Name);

            _context.Add(artist);
            await _context.SaveChangesAsync();

            var artistDto = _mapper.Map<ArtistDto>(artist);

            return new ApiResult<ArtistDto>( true, 201, artistDto, null);
        }
    }
}
