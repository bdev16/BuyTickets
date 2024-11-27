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
    public class FlightControllerViewFixture : IDisposable
    {
        public Enterprise Enterprise { get; private set; }
        public List<Flight> Flights { get; private set; }
        public List<Airport> Airports { get; private set; }
        public FlightController FlightController { get; private set; }
        public FlightControllerView FlightControllerView { get; private set; }

        public FlightControllerViewFixture()
        {
            // Inicializando dados compartilhados entre os testes
            Enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");

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

            FlightController = new FlightController(Flights);

            var globalValidations = new GlobalValidations();
            FlightControllerView = new FlightControllerView(FlightController, globalValidations, Airports);
        }

        public void Dispose()
        {
            // Libera recursos caso necessário, nada específico aqui
        }
    }
}