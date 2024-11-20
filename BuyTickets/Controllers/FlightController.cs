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
    public class FlightController : IController<Flight>
    {
        private List<Flight> _flights = new List<Flight>();
        public FlightController(List<Flight> flights)
        {
            _flights = flights;
        }
        public Flight Create(Flight flight)
        {
            if (flight == null)
            {
                return null;
            }
            _flights.Add(flight);
            return flight;
        }

        public List<Flight> SearchAll()
        {
            if (_flights.Count == 0)
            {
                return null;
            }
            
            return _flights;
        }

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

        public IEnumerable<Flight> FlightFilter(string origin, string destiny, dynamic date)
        {
            IEnumerable<Flight> result;

            if (origin != "" && destiny == "" && date == null)
            {
                result = _flights.Where(f => f.Origin == origin);
                return result;
            }

            if (destiny != "" && origin == "" && date == null)
            {
                result = _flights.Where(f => f.Destiny == destiny);
                return result;
            }

            if (date != null && origin == "" && destiny == "")
            {
                result = _flights.Where(f => f.Date == date);
                return result;
            }

            if (origin != "" && date != null &&  destiny == "")
            {
                result = _flights.Where(f => f.Origin == origin && f.Date == date);
                return result;
            }

            if (destiny != "" && date != null && origin == "")
            {
                result = _flights.Where(f => f.Destiny == destiny && f.Date == date);
                return result;
            }

            if (origin == "" && destiny == "" && date == null)
            {
                result = _flights;
                return result;
            }

            return null;
        }

        public Flight Update(Flight flightUpdate)
        {

            var flight = _flights.FirstOrDefault(f => f.Id == flightUpdate.Id);
            if (flight == null)
            {
                return null;
            }
            
            flight.Origin = flightUpdate.Origin;
            flight.Destiny = flightUpdate.Destiny;
            flight.Date = flightUpdate.Date;
            flight.DepartureTime = flightUpdate.DepartureTime;
            flight.ArrivalTime = flightUpdate.ArrivalTime;
            flight.Enterprise = flightUpdate.Enterprise;
            flight.registeredCustomers = flight.registeredCustomers;
            return flight;
        }

        public bool Delete(Guid idFlight)
        {
            var resultSearchById = _flights.FirstOrDefault(f => f.Id == idFlight);
            if (resultSearchById == null)
            {
                return false;
            }

            _flights.Remove(resultSearchById);
            return true;
        }

        public IEnumerable<Customer> CustomerList(Guid IdFlight)
        {
            var resultSearchById = _flights.FirstOrDefault(f => f.Id == IdFlight);
            if (resultSearchById == null)
            {
                return null;
            }
            else
            {
                if (resultSearchById.registeredCustomers.Count() == 0)
                {
                    return null;
                }

                return resultSearchById.registeredCustomers;
            }
        }
    }
}