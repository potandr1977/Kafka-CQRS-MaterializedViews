using Commands.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Commands.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator) => _mediator = mediator;

        // POST api/<PersonController>
        [HttpPost]
        public Task<Guid> Post([FromBody] CreatePersonCommand request) => 
            _mediator.Send(request);


        // PUT api/<PersonController>/5
        [HttpPut]
        public Task Put([FromBody] UpdatePersonCommand request) =>
            _mediator.Send(request);

        /*
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        */

        // DELETE api/<PersonController>/5
        [HttpDelete]
        public Task Delete([FromBody] DeletePersonCommand request) =>
            _mediator.Send(request);
    }
}
