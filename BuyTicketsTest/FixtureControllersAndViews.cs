using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.models;
using BuyTickets.Controllers;
using BuyTickets.views;
using BuyTickets;

namespace BuyTicketsTest
{
    public class FixtureControllersAndViews : IDisposable
    {
        public Customer Customer { get; private set; }
        public Enterprise Enterprise { get; private set; }
        public List<Flight> Flights { get; private set; }
        public List<Airport> Airports { get; private set; }
        public List<Customer> Customers { get; private set; }
        public List<Enterprise> Enterprises { get; private set; }
        public FlightController FlightController { get; private set; }
        public FlightControllerView FlightControllerView { get; private set; }
        public CustomerController CustomerController{ get; private set; }
        public CustomerControllerView CustomerControllerView{ get; private set; }
        public EnterpriseController EnterpriseController{ get; private set; }
        public EnterpriseControllerView EnterpriseControllerView{ get; private set; }
        public MenuView MenuView { get; private set; }

        public FixtureControllersAndViews()
        {
            // Inicializando dados compartilhados entre os testes
            Enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");
            Customer = new Customer("emailvalido@gmail.com", "cliente123", "Bruno", "Antonio", "56097045080"); 

            // Flights = new List<Flight>();

            Flights = new List<Flight>()
            {
                new Flight("RIO BRANCO (AC)", "MACAPA (AP)", "30/12/2024", "08:00", "10:00", Enterprise)
            };

            Airports = new List<Airport>
            {
                new Airport("RIO BRANCO", "AC"), new Airport("MACAPA", "AP"),
                new Airport("MANAUS", "AM"), new Airport("BELEM", "PA")
            };

            Enterprises = new List<Enterprise>();
            Enterprises.Add(Enterprise);

            Customers = new List<Customer>();
            Customers.Add(Customer);

            FlightController = new FlightController(Flights);
            EnterpriseController = new EnterpriseController(Enterprises);
            CustomerController = new CustomerController(Customers);

            var globalValidations = new GlobalValidations();
            FlightControllerView = new FlightControllerView(FlightController, globalValidations, Airports);
            EnterpriseControllerView = new EnterpriseControllerView(EnterpriseController, globalValidations);
            CustomerControllerView = new CustomerControllerView(CustomerController, FlightController, globalValidations);

            MenuView = new MenuView(FlightControllerView, EnterpriseControllerView, CustomerControllerView);
        }

        public void Dispose()
        {
            // Libera recursos caso necessário, nada específico aqui
        }
    }
}