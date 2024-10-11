using Flunt.Notifications;
using Flunt.Validations;
using BuyTickets.models;
using BuyTickets;
using System.Text.Json;
using System.Xml.XPath;
using BuyTickets.Controllers;
using BuyTickets.views;
using BuyTickets.Validations;

// Enterprise enterprise = new Enterprise(null, "latamairlines@gmail.com", "latam123");

// Verifier verifier = new Verifier();

// var result = verifier.Verify(enterprise);

// Console.WriteLine(JsonSerializer.Serialize(result));

List<Enterprise> enterprises = new List<Enterprise>();

// EnterpriseController enterpriseController = new EnterpriseController(enterprises);

// GlobalValidations globalValidations = new GlobalValidations();

// EnterpriseControllerView enterpriseControllerView = new EnterpriseControllerView(enterpriseController, globalValidations);

// enterpriseControllerView.Create();

foreach (var enterprise in enterprises)
{
    Console.WriteLine(enterprise);
}