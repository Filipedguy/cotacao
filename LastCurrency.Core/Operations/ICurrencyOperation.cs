using LastCurrencyAPI.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LastCurrencyAPI.Core.Operations
{
    public interface ICurrencyOperation
    {
        Currency GetCurrency(string currencyId);
    }
}
