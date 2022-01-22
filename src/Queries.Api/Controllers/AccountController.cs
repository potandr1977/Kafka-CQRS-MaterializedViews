using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Queries.Application.Accounts;
using Queries.Application.Persons;
using Queries.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Queries.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AccountController(
            IMediator mediator,
            IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public Task<IReadOnlyCollection<Account>> Get() =>
            _mediator.Send(new GetAllAccountQuery());


        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("GetLastUpdate")]
        public string GetLastUpdate() => _configuration["LastUpdate"];
    }
}
