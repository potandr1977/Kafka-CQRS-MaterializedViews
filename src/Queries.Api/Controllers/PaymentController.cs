using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Queries.Application.Payments;
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
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public PaymentController(
            IMediator mediator,
            IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public Task<IReadOnlyCollection<Payment>> Get() =>
            _mediator.Send(new GetAllPaymentQuery());


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
