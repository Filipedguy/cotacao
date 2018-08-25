using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LastCurrencyAPI.Core.Domain;
using LastCurrencyAPI.Core.Operations;
using Microsoft.AspNetCore.Mvc;

namespace LastCurrencyAPI.Application.Controllers
{
    [Route("Cotacao")]
    public class CotacaoController : Controller
    {
        private ICurrencyOperation _currencyOperation;

        public CotacaoController(ICurrencyOperation currencyOperation)
        {
            _currencyOperation = currencyOperation;
        }

        [HttpGet("USD")]
        public IActionResult Get()
        {
            Currency currencyResponse;

            try
            {
                currencyResponse = _currencyOperation.GetCurrency("USD");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(currencyResponse);
        }
    }
}
