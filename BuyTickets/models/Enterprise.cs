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
        private string email;
        public string Email { get { return email;} set { email = value; } }
        private string password;
        public string Password { get { return password;} set { password = value; } }
        public List<Flight> Flights { get; private set; }

        public Enterprise(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            Flights = new List<Flight>();  
        }
    }
}