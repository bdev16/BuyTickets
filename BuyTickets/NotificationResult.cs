using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets
{
    public class NotificationResult
    {
        public NotificationResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
    }
}