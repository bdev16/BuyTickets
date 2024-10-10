using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;


namespace BuyTickets.models
{
    public class Enterprise : Notifiable<Notification>
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
            var contract = new Contract<Notification>().
                Requires().
                IsNotNull(name, "Nome", "O nome nao pode ser vazio").
                IsNotNull(email, "Email", "O email nao pode ser vazio").
                IsEmail(email, "Email", "O email informado nao e valido").
                IsNotNull(password, "Senha", "A senha nao pode ser vazia");
                
            AddNotifications(contract); 
                                 
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            Flights = new List<Flight>();

        }
    }
}