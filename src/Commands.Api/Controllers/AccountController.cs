using Commands.Application.Commands;
using Domain.Models;
using Domain.Services;
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
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public Task<List<Account>> Get() => _mediator.Send(new GetAllAccountsQuery());

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public Task<Account> Get(int id) =>
            _mediator.Send(
                new GetAccountQuery() 
                { 
                    Id = Guid.NewGuid()
                });
        

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] CreateAccountCommand request) => _mediator.Send(request);

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
