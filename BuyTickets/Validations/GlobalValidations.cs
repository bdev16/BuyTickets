using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;

namespace BuyTickets.Validations
{
    public class GlobalValidations : Notifiable<Notification>
    {
        public NotificationResult CreateEnterpriseValidate(string name, string email, string password)
        {
            Clear();
            var contract = new Contract<Notification>().
                Requires().
                IsNotNull(name, "Nome", "O nome nao pode ser vazio").
                IsNotNull(email, "Email", "O email nao pode ser vazio").
                IsEmail(email, "Email", "O email informado nao e valido").
                IsNotNull(password, "Senha", "A senha nao pode ser vazia");
                
            AddNotifications(contract.Notifications);

            // var copyListNotifications = contract.Notifications;

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

        public NotificationResult CreateFlightValidate(string origin, string destiny, string date, string departureTime, string arrivalTime)
        {
            Clear();

            DateTime parsedDate;

            bool isValidDate = DateTime.TryParse(date, out parsedDate);
            bool isValidDepartureTime = DateTime.TryParse(departureTime, out parsedDate);
            bool isValidArrivalTime = DateTime.TryParse(arrivalTime, out parsedDate);

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