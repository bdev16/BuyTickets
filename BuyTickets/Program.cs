using Flunt.Notifications;
using Flunt.Validations;
using BuyTickets.models;
using BuyTickets;
using System.Text.Json;
using System.Xml.XPath;
using BuyTickets.Controllers;
using BuyTickets.views;
using BuyTickets.Validations;

 Enterprise enterprise = new Enterprise("Latam", "latamairlines@gmail.com", "latam123");

// // Verifier verifier = new Verifier();

// // var result = verifier.Verify(enterprise);

// // Console.WriteLine(JsonSerializer.Serialize(result));

 List<Enterprise> enterprises = new List<Enterprise>();

 enterprises.Add(enterprise);

 EnterpriseController enterpriseController = new EnterpriseController(enterprises);

 GlobalValidations globalValidations = new GlobalValidations();

//  EnterpriseControllerView enterpriseControllerView = new EnterpriseControllerView(enterpriseController, globalValidations);

// enterpriseControllerView.Create();

// foreach (var enterprise in enterprises)
// {
//     Console.WriteLine(enterprise);
// }

List<Flight> flights = new List<Flight>();

//  Flight flight = new Flight("SAO PAULO", "RECIFE", "dasfafhahagjg", "08:00", "10:00", enterprise);

 FlightController flightController = new FlightController(flights);

//  var result = flightController.Create(flight);

//  Console.WriteLine($"Voo {result.Id} foi criado com sucesso!\nData: {result.Date}; Horario de saida: {result.DepartureTime}; Horario de chegada: {result.ArrivalTime}");

 FlightControllerView flightControllerView = new FlightControllerView(flightController, globalValidations);

//  flightControllerView.Create(enterprise);

EnterpriseControllerView enterpriseControllerView = new EnterpriseControllerView(enterpriseController, globalValidations);

MenuView menuView = new MenuView(flightControllerView, enterpriseControllerView);

menuView.EnterpriseMenu(enterprise);








