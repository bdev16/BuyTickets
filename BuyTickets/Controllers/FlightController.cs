using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BuyTickets.interfaces;
using BuyTickets.models;

namespace BuyTickets.Controllers
{
    /// <summary>
    /// This class represents the business logic of the Flight class, containing the methods to Create, Update, List and remove a class from a Flight List
    /// </summary>
    public class FlightController : IController<Flight>
    {
        private List<Flight> _flights = new List<Flight>();

        /// <summary>
        /// Will add flight in the flight list.
        /// </summary>
        /// <param name="flight">Will receive a Flight class Object</param>
        public void Create(Flight flight)
        {
            if (flight == null)
            {
                Console.WriteLine("Ocorreu ao tentar criar o voo...");
            }
            _flights.Add(flight);
            Console.WriteLine($"Voo {flight.Id} criado com sucesso!!!");
        }

        /// <summary>
        /// This method provides access to a copy of the Flight list.
        /// </summary>
        /// <returns>Will return the flight list.</returns>
        public List<Flight> SearchAll()
        {
            return _flights;
        }

        /// <summary>
        /// You will search for a flight in the flight list using your Id attribute.
        /// </summary>
        /// <param name="idFlight">Will receive a Guid class which will represent the Flight ID attribute.</param>
        /// <returns>Will returning the found class</returns>
        public Flight SearchById(Guid idFlight)
        {
            var resultSearchById = _flights.FirstOrDefault(f => f.Id == idFlight);
            if (resultSearchById != null)
            {
                return resultSearchById;
            }
            else
            {
                return resultSearchById;
            }
        }

        /// <summary>
        /// It will modify the attributes of a Flight already present in the Flight List.
        /// </summary>
        /// <param name="flightUpdate">Will receive a Flight class Object.</param>
        public void Update(Flight flightUpdate)
        {

            var flight = _flights.FirstOrDefault(f => f.Id == flightUpdate.Id);
            if (flight != null)
            {
                flight.Origin = flightUpdate.Origin;
                flight.Destiny = flightUpdate.Destiny;
                flight.Date = flightUpdate.Date;
                flight.DepartureTime = flightUpdate.DepartureTime;
                flight.ArrivalTime = flightUpdate.ArrivalTime;
                flight.Enterprise = flightUpdate.Enterprise;
            }
            else
            {
                Console.WriteLine("O Codigo informado não corresponde a nenhum dos Voos cadastrados no sistema...");
            }
        }

        /// <summary>
        /// This method will remove the Flight class for the flight list. 
        /// </summary>
        /// <param name="idFlight">Will receive a Guid class which will represent the Flight ID attribute.</param>
        public void Delete(Guid idFlight)
        {
            var resultSearchById = _flights.FirstOrDefault(f => f.Id == idFlight);
            if (resultSearchById != null)
            {
                _flights.Remove(resultSearchById);
            }
            else
            {
                Console.WriteLine("O Codigo informado não corresponde a nenhum dos Voos cadastrados no sistema...");
            }
        }
    }
}