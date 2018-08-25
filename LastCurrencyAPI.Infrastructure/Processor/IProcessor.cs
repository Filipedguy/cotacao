using System;
using System.Collections.Generic;
using System.Text;

namespace LastCurrencyAPI.Infrastructure.Processor
{
    public interface IProcessor
    {
        void Start();
        void Stop();
    }
}
