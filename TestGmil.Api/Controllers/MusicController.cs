using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Queries;

namespace TestGmil.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusicController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MusicController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateMusicCommand command)
        {
            var result = await _mediator.Send(command);

            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new FindMusicsQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new FindMusicByIdQuery(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateMusicCommand command)
        {
            var result = await _mediator.Send(new UpdateMusicCommand(id, command.Title));
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteMusicCommand(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}