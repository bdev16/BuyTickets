using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.models
{
    public class Enterprise : User
    {
        private string cnpj;
        public string Cnpj { get { return cnpj; } set { cnpj = value; } }
        public List<Flight> Flights { get; private set; }

        public Enterprise(string fullName, string email, string password, string cnpj) : base(fullName, email, password)
        {
            Cnpj = cnpj;
            Flights = new List<Flight>();  
        }
    }
}