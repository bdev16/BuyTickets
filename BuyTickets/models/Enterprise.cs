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
        private List<Flight> flights;
        private List<Customer> clients;

        public Enterprise(string fullName, string email, string password, string cnpj) : base(fullName, email, password)
        {
            Cnpj = cnpj;
            flights = new List<Flight>();
            clients = new List<Customer>();
        }
    }
}