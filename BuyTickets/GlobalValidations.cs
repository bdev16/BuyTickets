using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;

namespace BuyTickets
{
    public class GlobalValidations : Notifiable<Notification>
    {
        public NotificationResult CreateFlightValidate(string origin, string destiny, string date, string departureTime, string arrivalTime)
        {
            //O metodo Clear limpa todas as notificações que foram geradas e adicionadas em Notifiable
            Clear();
            //Cria uma variavel do tipo DateTime que vai servir como uma referencia
            DateTime dateFormat;
            //Os codigos abaixo tentam fazer a conversão dos valores que vão ser utilizados para o tipo datetime
            bool isValidDate = DateTime.TryParse(date, out dateFormat);
            bool isValidDepartureTime = DateTime.TryParse(departureTime, out dateFormat);
            bool isValidArrivalTime = DateTime.TryParse(arrivalTime, out dateFormat);
            var contract = new Contract<Notification>().
                Requires().
                IsNotNull(origin, "Origem", "O local de origem nao pode ser vazio").
                IsNotNull(destiny, "Destino", "O local de destino nao pode ser vazio").
                IsNotNull(date, "Data", "A data do voo nao pode ser vazia").
                IsTrue(isValidDate, "Data", "A data informada nao e uma data valida").
                IsNotNull(departureTime, "Horario de saida", "O horario de saida nao pode ser vazio").
                IsTrue(isValidDepartureTime, "Horario de saida", "O horario de saida informado nao segue a estrutura de um horario valida").
                IsNotNull(arrivalTime, "Horario de chegada", "O horario de chegada nao pode ser vazio").
                IsTrue(isValidArrivalTime, "Horario de chegada", "O horario de chegada informado nao segue a estrutura de um horario valida");
            
            AddNotifications(contract);
            
            if(!IsValid)
            {
                return new NotificationResult(
                    false,
                    "Alguns erros foram gerados",
                    Notifications         
                );  
            }
            return new NotificationResult(
                true,
                "Mensagem",
                null
            );
        }
    }
}