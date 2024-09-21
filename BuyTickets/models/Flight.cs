using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.models
{
    public class Flight
    {
        public Guid Id { get; private set; }
        private string origin;
        public string Origin { get { return origin; } set { origin = value; } }
        private string destiny;
        public string Destiny { get { return destiny; } set { destiny = value; } }
        private DateTime date;
        public DateTime Date { get { return date; } set { date = value; } }
        private DateTime departureTime;
        public DateTime DepartureTime { get { return departureTime; } set { departureTime = value; } }
        private DateTime arrivalTime;
        public DateTime ArrivalTime  { get { return arrivalTime; } set { arrivalTime = value; } }
        public Enterprise Enterprise{ get; private set; }

        public Flight(string origin, string destiny, DateTime date, DateTime departureTime, DateTime arrivalTime, Enterprise enterprise)
        {
            Id = Guid.NewGuid();
            Origin = origin;
            Destiny = destiny;
            Date = date;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Enterprise = enterprise;
        }
    }
}