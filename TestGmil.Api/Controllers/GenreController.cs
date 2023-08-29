using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Queries;

namespace TestGmil.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {

        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);

            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new FindGenresQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new FindGenreByIdQuery(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateGenreCommand command)
        {
            var result = await _mediator.Send(new UpdateGenreCommand(id, command.Name));
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteGenreCommand(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}