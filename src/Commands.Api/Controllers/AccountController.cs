using Commands.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Commands.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator) => _mediator = mediator;

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] CreateAccountCommand request) => _mediator.Send(request);

        // PUT api/<AccountController>/5
        [HttpPut]
        public void Put([FromBody] UpdateAccountCommand request) => _mediator.Send(request);

        // DELETE api/<AccountController>/5
        [HttpDelete]
        public void Delete([FromBody] DeleteAccountCommand request) => _mediator.Send(request);
    }
}
