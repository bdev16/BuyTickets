using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;

namespace BuyTicketsTest
{
    public class FlightControllerFixture
    {
        public Flight Flight { get; private set; }
        public Enterprise Enterprise { get; private set; }
        public FlightController FlightController { get; private set; }
        public List<Flight> Flights { get; private set; }
        public List<Airport> Airports { get; private set; }

        public FlightControllerFixture()
        {
            Enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");

            Flights = new List<Flight>()
            {
                new Flight("RIO BRANCO (AC)", "MACAPA (AP)", "30/12/2024", "08:00", "10:00", Enterprise)
            };

            FlightController = new FlightController(Flights);
        }
    }
}