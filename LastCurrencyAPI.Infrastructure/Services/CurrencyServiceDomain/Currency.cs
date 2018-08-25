using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LastCurrencyAPI.Infrastructure.Services.CurrencyServiceDomain
{
    public class Currency
    {
        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("valor")]
        public float Value { get; set; }

        [JsonProperty("ultima_consulta")]
        public int LastUpdate { get; set; }

        [JsonProperty("fonte")]
        public string Source { get; set; }
    }
}
