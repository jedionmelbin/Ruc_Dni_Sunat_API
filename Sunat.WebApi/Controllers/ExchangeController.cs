using Microsoft.AspNetCore.Mvc;
using Sunat.WebApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sunat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        public ExchangeController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }
        // GET api/<ExchangeController>/5
        [HttpGet()]
        public decimal GetAsync()
        {

            decimal response =  _exchangeRateService.ExchangeRate();
            return response;
        }
    }
}
