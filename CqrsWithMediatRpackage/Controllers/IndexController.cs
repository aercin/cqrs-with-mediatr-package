using CqrsWithMediatRpackage.Commands;
using CqrsWithMediatRpackage.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsWithMediatRpackage.Controllers
{
    [Route("api/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IndexController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("/create/user")]
        public async Task<IActionResult> CreateUser(UserCreate.Command cmd)
        {
            var result = await this._mediator.Send(cmd);

            if (result != null)
                await this._mediator.Publish(new UserCreate.Event(result.id));

            return Ok(result);
        }

        [HttpGet("/list/user")]
        public async Task<ActionResult> ListUserByAge([FromQuery] int age)
        {
            var result = await this._mediator.Send(new GetUserByAge.Query(age));
            return Ok(result);
        }
    }
}
