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

List<Airport> airports = new List<Airport>
{
    new Airport("RIO BRANCO", "AC"), new Airport("MACAPA", "AP"),
    new Airport("MANAUS", "AM"), new Airport("BELEM", "PA"),
    new Airport("PORTO VELHO", "RO"), new Airport("BOA VISTA", "RR"),
    new Airport("PALMAS", "TO"), new Airport("MACEIO", "AL"),
    new Airport("SALVADOR", "BA"), new Airport("FORTALEZA", "CE"),
    new Airport("SAO LUIZ",  "(MA)"), new Airport("JOAO PESSOA", "PB"),
    new Airport("RECIFE", "PE"), new Airport("TERESINA", "PI"),
    new Airport("NATAL", "RN"), new Airport("ARACAJU", "SE"),
    new Airport("BRASILIA", "DF"), new Airport("GOIANIA", "GO"),
    new Airport("CUIABA", "MT"), new Airport("CAMPO GRANDE", "MS"),
    new Airport("VITORIA", "ES"), new Airport("BELO HORIZONTE", "MG"),
    new Airport("RIO DE JANEIRO", "RJ"), new Airport("SAO PAULO", "SP"),
    new Airport("CURITIBA", "PR"), new Airport("PORTO ALEGRE", "RS"),
    new Airport("FLORIANOPOLIS", "SC"), new Airport("JOAO PESSOA", "PB"),
    new Airport("RECIFE", "PE"), new Airport("TERESINA", "PI"),
    new Airport("NATAL", "RN"), new Airport("ARACAJU", "SE"),
    new Airport("BRASILIA", "DF"), new Airport("GOIANIA", "GO"),
    new Airport("CUIABA", "MT"), new Airport("CAMPO GRANDE", "MS"),
    new Airport("VITORIA", "ES"), new Airport("BELO HORIZONTE", "MG"),
    new Airport("RIO DE JANEIRO", "RJ"), new Airport("SAO PAULO", "SP"),
    new Airport("CURITIBA", "PR"), new Airport("PORTO ALEGRE", "RS"),
    new Airport("FLORIANOPOLIS", "SC"),
};

var resultValueInAirports = airports.FirstOrDefault(a => a.City == "CURITIBA");

Customer customer = new Customer("emailvalido@gmail.com", "cliente123", "Bruno", "Antonio", "56097045080");
customers.Add(customer);

GlobalValidations globalValidations = new GlobalValidations();
EnterpriseController enterpriseController = new EnterpriseController(enterprises);
EnterpriseControllerView enterpriseControllerView = new EnterpriseControllerView(enterpriseController, globalValidations);
FlightController flightController = new FlightController(flights);
FlightControllerView flightControllerView = new FlightControllerView(flightController, globalValidations, airports);
CustomerController customerController= new CustomerController(customers);
CustomerControllerView customerControllerView = new CustomerControllerView(customerController,flightController, globalValidations);

MenuView menuView = new MenuView(flightControllerView, enterpriseControllerView, customerControllerView);

menuView.MainMenu(menuView);
// menuView.EnterpriseMenu(enterprise);