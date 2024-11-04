using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.models
{
    public class Passage 
    {
        public Guid Id { get; private set; }
        private string customerName;
        public string CustomerName { get { return customerName; } set { customerName = value; } }
        private string customerFirstName;
        public string CustomerFirstName { get { return customerFirstName; } set { customerFirstName = value; } }
        private string customerLastName;
        public string CustomerLastName { get { return customerLastName; } set { customerLastName = value; } }
        private string customerCpf;
        public string CustomerCpf { get { return customerCpf; } set { customerCpf = value; } }
        public Flight PurchasedFlight { get; private set; }
        public Customer Buyer { get; private set; }

        public Passage(string customerName, string customerFirstName, string customerLastName, string customerCpf, Flight purchasedFlight, Customer buyer)
        {
            Id = new Guid();
            CustomerName = customerName;
            CustomerFirstName = customerFirstName;
            CustomerLastName = customerLastName;
            CustomerCpf = customerCpf;
            PurchasedFlight = purchasedFlight;
            Buyer = buyer;
        }
    }
}