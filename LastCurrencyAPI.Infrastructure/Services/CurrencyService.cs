using LastCurrencyAPI.Core.Domain;
using LastCurrencyAPI.Core.Domain.Exceptions;
using LastCurrencyAPI.Core.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LastCurrencyAPI.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        private ILogger<CurrencyService> _logger;
        private HttpClient _httpClient;

        public CurrencyService(IConfiguration configuration, ILogger<CurrencyService> logger)
        {
            _logger = logger;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration["Services:Currency:Address"]);
        }

        public async Task<Currency> GetCurrency(string currencyId)
        {
            var currencyCollection = await GetCurrencyCollection();

            var currency = (currencyCollection as List<Currency>).Find(cur => cur.Id == currencyId);

            if (currency == null)
            {
                throw new ServiceRequestFailedException("CurrencyService", "Cannot support the requested currency", null);
            }

            return currency;
        }

        public async Task<ICollection<Currency>> GetCurrencyCollection()
        {
            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "cotacao/v1/valores"))
                {
                    var response = await _httpClient.SendAsync(request);

                    var json = await response.Content.ReadAsStringAsync();

                    var responseObj = JsonConvert.DeserializeObject<CurrencyServiceDomain.CurrencyResponse>(json);

                    var ret = ConvertResponse(responseObj);

                    return ret;
                }
            }
            catch (Exception ex)
            {
                var alert = new Alert
                {
                    Message = $"Error on requesting the last currencys from currency service",
                    Exception = ex
                };

                _logger.LogError(ex, alert.Message);

                throw new ServiceRequestFailedException("CurrencyService", alert.Message, ex);
            }
        }

        private ICollection<Currency> ConvertResponse(CurrencyServiceDomain.CurrencyResponse currencyResponse)
        {
            return new List<Currency>
                    {
                        new Currency()
                        {
                            Id = "ARS",
                            Name = currencyResponse.Values.ARS.Name,
                            Value = currencyResponse.Values.ARS.Value,
                            LastUpdate = currencyResponse.Values.ARS.LastUpdate,
                            Source = currencyResponse.Values.ARS.Source
                        },
                        new Currency()
                        {
                            Id = "BTC",
                            Name = currencyResponse.Values.BTC.Name,
                            Value = currencyResponse.Values.BTC.Value,
                            LastUpdate = currencyResponse.Values.BTC.LastUpdate,
                            Source = currencyResponse.Values.BTC.Source
                        },
                        new Currency()
                        {
                            Id = "EUR",
                            Name = currencyResponse.Values.EUR.Name,
                            Value = currencyResponse.Values.EUR.Value,
                            LastUpdate = currencyResponse.Values.EUR.LastUpdate,
                            Source = currencyResponse.Values.EUR.Source
                        },
                        new Currency()
                        {
                            Id = "GBP",
                            Name = currencyResponse.Values.GBP.Name,
                            Value = currencyResponse.Values.GBP.Value,
                            LastUpdate = currencyResponse.Values.GBP.LastUpdate,
                            Source = currencyResponse.Values.GBP.Source
                        },
                        new Currency()
                        {
                            Id = "USD",
                            Name = currencyResponse.Values.USD.Name,
                            Value = currencyResponse.Values.USD.Value,
                            LastUpdate = currencyResponse.Values.USD.LastUpdate,
                            Source = currencyResponse.Values.USD.Source
                        }
                    };
        }
    }
}
