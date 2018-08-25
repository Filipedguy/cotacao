using LastCurrencyAPI.Core.Domain;
using LastCurrencyAPI.Core.Domain.Exceptions;
using LastCurrencyAPI.Core.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LastCurrencyAPI.Infrastructure.Processor.Currency
{
    public class USDProcessor : AbstractProcessor
    {
        private ICurrencyService _currencyService;

        public USDProcessor(ILogger<USDProcessor> logger, IMemoryCache memoryCache, Queue<Alert> alertQueue, ICurrencyService currencyService) : base(logger, memoryCache, alertQueue)
        {
            _currencyService = currencyService;
        }

        protected override void TimerCallback(object state)
        {
            try
            {
                var result = _currencyService.GetCurrency("USD").Result;

                MemoryCache.Set("USD", result);
            }
            catch (ServiceRequestFailedException srfex)
            {
                Alert.EnqueueNewAlert(ref AlertQueue, srfex.Message, srfex);
            }
            catch (Exception ex)
            {
                Logger.LogError("Unexpected error while updating the USD currency", ex);
                Alert.EnqueueNewAlert(ref AlertQueue, "Unexpected error while updating the USD currency", ex);
            }
        }
    }
}
