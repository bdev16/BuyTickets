using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.models;

namespace BuyTickets.Controllers
{
    public class CustomerController
    {
        private List<Customer> _customers;

        public CustomerController(List<Customer> customers)
        {
            _customers = customers;
        }

        public Customer Create(Customer customer)
        {
            if (customer == null)
            {
                return null;
            }

            _customers.Add(customer);
            return customer;
        }

        public Customer SearchById(Guid idCustomer)
        {
            var resultSearchById = _customers.FirstOrDefault(c => c.Id == idCustomer);
             if (resultSearchById != null)
            {
                return resultSearchById;
            }
            else
            {
                return resultSearchById;
            }
        }

        public Customer Update(Customer customerUpdate)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == customerUpdate.Id);
            if (customer != null)
            {
                customer.FullName = customerUpdate.FullName;
                return customer;
            }
            else
            {
                return null;
            }
        }

        public bool Delete(Guid idCustomer)
        {
            var resultSearchById = _customers.FirstOrDefault(c => c.Id == idCustomer);
            if (resultSearchById != null)
            {
                _customers.Remove(resultSearchById);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Customer Login(string emailCustomer, string passwordCustomer)
        {
            var resultSearchByEmailAndPassword = _customers.FirstOrDefault(c => c.Email == emailCustomer && c.Password == passwordCustomer);
            if (resultSearchByEmailAndPassword == null)
            {
                return null;
            }

            return resultSearchByEmailAndPassword;
        }
    }
}