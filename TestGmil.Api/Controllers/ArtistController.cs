using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Queries;

namespace TestGmil.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ArtistController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateArtistCommand command)
        {
            var result = await _mediator.Send(command);
            
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new FindArtistsQuery());
           
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new FindArtistByIdQuery(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateArtistCommand command)
        {
            var result = await _mediator.Send(new UpdateArtistCommand(id, command.Name));
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteArtistCommand(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}