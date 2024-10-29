using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.models
{
    public class Customer : User
    {
        private string firstName;
        public string FirstName { get { return firstName; } set { firstName = value; } }
        private string lastName;
        public string LastName { get { return lastName;} set { lastName = value; } }
        private string cpf;
        public string Cpf { get { return cpf;} set { cpf = value; } }
        public List<Flight> purchasedFlights { get; private set;}

        public Customer(string email, string password, string firstName, string lastName, string cpf) : base($"{firstName} {lastName}", email, password)
        {
            FirstName = firstName;
            LastName = lastName;
            Cpf = cpf;
            purchasedFlights = new List<Flight>();
        }
    }
}