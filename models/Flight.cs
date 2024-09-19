using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.models
{
    public class Flight
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Origin { get; private set; }
        public string Destiny { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public Enterprise Enterprise{ get; private set; }
    }
}