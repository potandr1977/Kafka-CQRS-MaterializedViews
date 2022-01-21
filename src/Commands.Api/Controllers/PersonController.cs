using Commands.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Commands.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/<PersonController>
        [HttpPost]
        public void Post([FromBody] CreatePersonCommand request) => 
            _mediator.Send(request);


        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] UpdatePersonCommand request) =>
            _mediator.Send(request);

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
