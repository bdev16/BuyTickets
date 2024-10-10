using Flunt.Notifications;
using Flunt.Validations;
using BuyTickets.models;
using BuyTickets;
using System.Text.Json;

Enterprise enterprise = new Enterprise(null, "latamairlines@gmail.com", "latam123");

Verifier verifier = new Verifier();

var result = verifier.Verify(enterprise);

Console.WriteLine(JsonSerializer.Serialize(result));
Console.WriteLine($"{result.Data.ToString()}");