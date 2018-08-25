using LastCurrencyAPI.Core.Domain;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace LastCurrencyAPI.Core.Operations
{
    public class CurrencyOperation : ICurrencyOperation
    {
        private IMemoryCache _memoryCache;

        public CurrencyOperation(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Currency GetCurrency(string currencyId)
        {
            Currency ret;

            if (_memoryCache.TryGetValue(currencyId, out ret) == false)
            {
                throw new ApplicationException("Currency not supported");
            }

            return ret;
        }
    }
}
