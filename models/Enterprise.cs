using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.models
{
    public class Enterprise
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<Flight> Flights { get; private set; }
    }
}