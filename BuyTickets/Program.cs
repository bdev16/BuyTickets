using BuyTickets;
using BuyTickets.Controllers;
using BuyTickets.models;
using BuyTickets.views;

List<Flight> flights = new List<Flight>();
List<Enterprise> enterprises = new List<Enterprise>();

Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123");
enterprises.Add(enterprise);

GlobalValidations globalValidations = new GlobalValidations();
EnterpriseController enterpriseController = new EnterpriseController(enterprises);
EnterpriseControllerView enterpriseControllerView = new EnterpriseControllerView(enterpriseController, globalValidations);
FlightController flightController = new FlightController(flights);
FlightControllerView flightControllerView = new FlightControllerView(flightController, globalValidations);

MenuView menuView = new MenuView(flightControllerView, enterpriseControllerView);

menuView.EnterpriseMenu(enterprise);