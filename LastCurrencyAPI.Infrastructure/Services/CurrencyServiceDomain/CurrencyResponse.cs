using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LastCurrencyAPI.Infrastructure.Services.CurrencyServiceDomain
{
    public class CurrencyResponse
    {
        public bool Status { get; set; }

        [JsonProperty("valores")]
        public Values Values { get; set; }
    }
}
