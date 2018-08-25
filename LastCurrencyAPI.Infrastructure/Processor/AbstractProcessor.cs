using LastCurrencyAPI.Core.Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LastCurrencyAPI.Infrastructure.Processor
{
    public abstract class AbstractProcessor : IProcessor
    {
        protected Queue<Alert> AlertQueue;
        protected IMemoryCache MemoryCache;
        protected ILogger<AbstractProcessor> Logger;

        private Thread _thread;
        private Timer _timer;

        public AbstractProcessor(ILogger<AbstractProcessor> logger, IMemoryCache memoryCache, Queue<Alert> alertQueue)
        {
            Logger = logger;
            MemoryCache = memoryCache;
            AlertQueue = alertQueue;
        }

        public void Start()
        {
            if (_thread != null && _thread.IsAlive)
            {
                Stop();
            }

            _thread = new Thread(ThreadStart);
            _thread.Start();
        }

        public void Stop()
        {
            _timer.Dispose();

            while (_thread.IsAlive)
            {
                // Wait thread to stop
            }

            _thread = null;
        }

        private void ThreadStart()
        {
            _timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        protected abstract void TimerCallback(object state);
    }
}
