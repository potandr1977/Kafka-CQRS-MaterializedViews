using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExchangeRateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        // GET: api/<ExchangeRateController>
        [HttpGet]
        public int Get()
        {
            if (new Random().Next(2) == 1)
            {
                throw new Exception("Exception to check AutoPolly on client side.");
            }

            return 77;
        }
    }
}
