using LastCurrencyAPI.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LastCurrencyAPI.Core.Services
{
    public interface ICurrencyService
    {
        Task<ICollection<Currency>> GetCurrencyCollection();

        Task<Currency> GetCurrency(string currencyId);
    }
}
