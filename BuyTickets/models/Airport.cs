using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.models
{
    public class Airport
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }
        private string city;
        public string City { get { return city; } set { city = value; } }
        private string state;
        public string State { get { return state; } set { state = value; } }
    
        public Airport(string city, string state)
        {
            Name = $"{city} ({state})";
            City = city.ToUpper();
            State = state.ToUpper();
        }
    }
}