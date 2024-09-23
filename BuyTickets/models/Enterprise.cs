using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.models
{
    public class Enterprise
    {
        public Guid Id { get; private set; }
        private string name;
        public string Name { get { return name; } set { name = value; } }
        public List<Flight> Flights { get; private set; }

        public Enterprise(string name)
        {
            Id = Guid.NewGuid();
            Name = name;   
        }
    }
}