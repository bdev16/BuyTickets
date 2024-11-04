using BuyTickets;
using BuyTickets.Controllers;
using BuyTickets.models;
using BuyTickets.views;

List<Flight> flights = new List<Flight>();
List<Enterprise> enterprises = new List<Enterprise>();
List<Customer> customers = new List<Customer>();

Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");
enterprises.Add(enterprise);

List<Flight> flightList = new List<Flight>
{
    new Flight("São Paulo", "Rio de Janeiro", "2024-11-10", "08:00", "10:00", enterprise),
    new Flight("Belo Horizonte", "Salvador", "2024-11-11", "14:00", "16:30", enterprise),
    new Flight("Curitiba", "Florianópolis", "2024-11-12", "09:30", "11:00", enterprise),
    new Flight("Brasília", "Fortaleza", "2024-11-13", "18:00", "20:30", enterprise),
    new Flight("Manaus", "Belém", "2024-11-14", "12:00", "14:00", enterprise),
};

flights = flightList;

Customer customer = new Customer("emailvalido@gmail.com", "cliente123", "Bruno", "Antonio", "56097045080");
customers.Add(customer);

GlobalValidations globalValidations = new GlobalValidations();
EnterpriseController enterpriseController = new EnterpriseController(enterprises);
EnterpriseControllerView enterpriseControllerView = new EnterpriseControllerView(enterpriseController, globalValidations);
FlightController flightController = new FlightController(flights);
FlightControllerView flightControllerView = new FlightControllerView(flightController, globalValidations);
CustomerController customerController= new CustomerController(customers);
CustomerControllerView customerControllerView = new CustomerControllerView(customerController,flightController, globalValidations);

MenuView menuView = new MenuView(flightControllerView, enterpriseControllerView, customerControllerView);

menuView.MainMenu(menuView);
// menuView.EnterpriseMenu(enterprise);