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

DateTime formatData2 = new DateTime();

DateTime formatData;

bool result = DateTime.TryParse("sdadsada", out formatData);

Console.WriteLine($"Formato de data: {result}, Formato de data: {formatData2}");

DateTime date = DateTime.Now;

string departureTime = "08:00:00";
string arrivalTime = "09:00:00";

string stringDate = $"{date}";

DateTime Date = DateTime.Parse(stringDate);
//Abaixo são criadas variaveis secundarias que vão servir para capturar
//Somente a parte da data sem utilizar o horario e utiliza-lá para montar
//Adicionar nas variaveis departureTime e arrivalTime
var dateInformed = stringDate.Split(' ');
var dateCaptured = dateInformed[0];
var departureTimeWithDateInclude = $"{dateCaptured} {departureTime}";
var arrivalTimeWithDateInclude = $"{dateCaptured} {arrivalTime}";

Console.WriteLine($"Data: {Date}; Horario de saida: {departureTimeWithDateInclude}, Horario de chegada: {arrivalTimeWithDateInclude}");

Console.ReadKey();

menuView.MainMenu(menuView);
// menuView.EnterpriseMenu(enterprise);