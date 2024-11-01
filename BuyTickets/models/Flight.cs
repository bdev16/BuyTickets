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
        private Enterprise enterprise;
        public Enterprise Enterprise{ get { return enterprise; } set { enterprise = value; } }
        List<Customer> registeredCustomers;
        public Flight(string origin, string destiny, string date, string departureTime, string arrivalTime, Enterprise enterprise)
        {
            Id = Guid.NewGuid();
            Origin = origin;
            Destiny = destiny;
            Date = DateTime.Parse(date);
            //Abaixo são criadas variaveis secundarias que vão servir para capturar
            //Somente a parte da data sem utilizar o horario e utiliza-lá para montar
            //Adicionar nas variaveis departureTime e arrivalTime
            var dateInformed = date.Split(' ');
            var dateCaptured = dateInformed[0];
            var departureTimeWithDateInclude = $"{dateCaptured} {departureTime}";
            var arrivalTimeWithDateInclude = $"{dateCaptured} {arrivalTime}";
            DepartureTime = DateTime.Parse(departureTimeWithDateInclude);
            ArrivalTime = DateTime.Parse(arrivalTimeWithDateInclude);
            Enterprise = enterprise;
            registeredCustomers = new List<Customer>();
        }
    }
}