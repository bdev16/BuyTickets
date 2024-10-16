using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        public Flight(string origin, string destiny, string date, string departureTime, string arrivalTime, Enterprise enterprise)
        {
            Id = Guid.NewGuid();
            Origin = origin.ToUpper();
            Destiny = destiny.ToUpper();
            //Abaixo é criada uma variavel do tipo DateTime, que vai servir para verificar se os valores
            //Informados do tipo string estão na estrutura correta para serem convertidos em valor do tipo DatTime
            DateTime dateFormat;
            if (DateTime.TryParse(date, out dateFormat))
            {
                Date = DateTime.Parse(date);
            }
            else
            {
                throw new FormatException("O valor referente a data do voo informado não está dentro da estrutura correta de uma data(dd/mm/yyyy 00:00)");
            }   
            //Abaixo são criadas variaveis secundarias que vão servir para capturar
            //Somente a parte da data sem utilizar o horario e utiliza-lá para montar
            //Adicionar nas variaveis departureTime e arrivalTime
            var dateInformed = date.Split(' ');
            var dateCaptured = dateInformed[0];
            var departureTimeWithDateInclude = $"{dateCaptured} {departureTime}";
            if (DateTime.TryParse(departureTime, out dateFormat))
            {
                DepartureTime = DateTime.Parse(departureTimeWithDateInclude);
            }
            else
            {
                throw new FormatException("O valor referente ao horario de saida do voo informado não está dentro da estrutura correta de um horario(00:00)");
            }
            
            var arrivalTimeWithDateInclude = $"{dateCaptured} {arrivalTime}";
             if (DateTime.TryParse(arrivalTime, out dateFormat))
            {
                ArrivalTime = DateTime.Parse(arrivalTimeWithDateInclude);
            }
            else
            {
                throw new FormatException("O valor referente ao horario de chegada do voo informado não está dentro da estrutura correta de um horario(00:00)");
            }

            Enterprise = enterprise;
        }
    }
}