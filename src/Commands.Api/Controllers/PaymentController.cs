using Commands.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Commands.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator) => _mediator = mediator;

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] CreatePaymentCommand request) => _mediator.Send(request);

        // PUT api/<AccountController>/5
        [HttpPut]
        public void Put([FromBody] UpdatePaymentCommand request) => _mediator.Send(request);

        // DELETE api/<AccountController>/5
        [HttpDelete]
        public void Delete([FromBody] DeletePaymentCommand request) => _mediator.Send(request);
    }
}
