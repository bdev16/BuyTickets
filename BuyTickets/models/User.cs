using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.models
{
    public abstract class User
    {
        public Guid Id { get; private set; }
        private string fullName;
        public string FullName { get { return fullName; } set { fullName = value; } }
        private string email;
        public string Email { get { return email;} set { email = value; } }
        private string password;
        public string Password { get { return password;} set { password = value; } }

        public User (string fullName, string email, string password)
        {
            Id = Guid.NewGuid();
            FullName = fullName.ToUpper();
            Email = email;
            Password = password;
        }
    }
}