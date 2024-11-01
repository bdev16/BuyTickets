using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.models;
using BuyTickets.Controllers;

namespace BuyTicketsTest
{
    public class CustomerControllerTest
    {
        [Fact]
        public void Check_CreateMethod_Add_EnterpriseObjectToList()
        {
            //Arrange
            
            List<Customer> customers = new List<Customer>();
            CustomerController customerController = new CustomerController(customers);
            Customer customer = new Customer("Bruno", "Antonio", "emailvalido@gmail.com", "bruno123", "89056091030");

            //Act

            customerController.Create(customer);

            var customerResult = customerController.SearchById(customer.Id);
            
            //Assert

            Assert.Equal(customer, customerResult);
        }

        [Fact]
        public void Check_MethodSearchById_ReturnTheEnterpriseInformed()
        {
            // Arrange

            List<Customer> customers = new List<Customer>();
            CustomerController customerController = new CustomerController(customers);
            Customer customer = new Customer("Bruno", "Antonio", "emailvalido@gmail.com", "bruno123", "89056091030");
            Customer customer2 = new Customer("Robson", "Silva", "emailvalido@gmail.com", "robson123", "65056041090");

            // Act

            customerController.Create(customer);
            customerController.Create(customer2);

            var customerResult = customerController.SearchById(customer.Id);

            // Assert

            Assert.Equal(customerResult, customer);
        }

        [Fact]
        public void Check_MethodUpdate_ModifieTheFlightRegisteredByTheFlightInformed()
        {
            // Arrange

            List<Customer> customers = new List<Customer>();
            CustomerController customerController = new CustomerController(customers);
            Customer customer = new Customer("Bruno", "Antonio", "emailvalido@gmail.com", "bruno123", "89056091030");
            Customer customerCopy = new Customer("Bruno", "Antonio", "emailvalido@gmail.com", "bruno123", "89056091030");

            // Act

            customerController.Create(customer);

            customer.FullName = "Bruno Antonio";

            customerController.Update(customer);

            var customerResultAfterUpdate = customerController.SearchById(customer.Id);

            // Assert

            Assert.NotEqual(customerResultAfterUpdate.FullName, customerCopy.FullName);
            Assert.Equal(customerResultAfterUpdate.Id, customer.Id);
        }

        [Fact]
        public void Check_MethodDelete_RemoveInformedEnterpriseFromEnterpriseList()
        {
            // Arrange

            List<Customer> customers = new List<Customer>();
            CustomerController customerController = new CustomerController(customers);
            Customer customer = new Customer("Bruno", "Antonio", "emailvalido@gmail.com", "bruno123", "89056091030");

            // Act

            customerController.Create(customer);

            var customerResult = customerController.SearchById(customer.Id);

            customerController.Delete(customer.Id);

            var customerResultBeforeRemoved = customerController.SearchById(customerResult.Id);

            // Assert

            Assert.Null(customerResultBeforeRemoved);
        }

        [Fact]
        public void Check_MethodLogin_ReturnsCustomerCaseLoginSuccess()
        {
            // Arrange

            List<Customer> customers = new List<Customer>();
            CustomerController customerController = new CustomerController(customers);
            Customer customer = new Customer("Bruno", "Antonio", "emailvalido@gmail.com", "bruno123", "89056091030");
        
            // Act

            customerController.Create(customer);

            var loginResult = customerController.Login(customer.Email, customer.Password);

            // Assert

            Assert.NotNull(loginResult);
        }
    }
}