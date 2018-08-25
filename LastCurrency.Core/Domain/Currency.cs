using System;
using System.Collections.Generic;
using System.Text;

namespace LastCurrencyAPI.Core.Domain
{
    public class Currency
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public float Value { get; set; }

        public int LastUpdate { get; set; }

        public string Source { get; set; }
    }
}
