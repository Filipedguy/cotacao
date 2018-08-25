using System;
using System.Collections.Generic;
using System.Text;

namespace LastCurrencyAPI.Core.Domain
{
    public class Alert
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public static void EnqueueNewAlert(ref Queue<Alert> queue, string message, Exception ex)
        {
            var alert = new Alert
            {
                Message = message,
                Exception = ex
            };

            queue.Enqueue(alert);
        }
    }
}
