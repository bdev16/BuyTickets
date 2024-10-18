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
            bool isNotValidFormatDate = DateTime.TryParse(date, out dateFormat);
            // bool isValidDate = DateTime.Parse(date) < DateTime.Now ? false : true;

            var dateInformed = date.Split(' ');
            var dateCaptured = dateInformed[0];
            var departureTimeWithDateInclude = $"{dateCaptured} {departureTime}";
            var arrivalTimeWithDateInclude = $"{dateCaptured} {arrivalTime}";

            bool isNotValidDepartureTime = DateTime.TryParse(departureTimeWithDateInclude, out dateFormat);
            bool isNotValidArrivalTime = DateTime.TryParse(arrivalTimeWithDateInclude, out dateFormat);
            
            var contract = new Contract<Notification>().
                Requires().
                IsNotNullOrEmpty(origin, "Origem", "O local de origem nao pode ser vazio").
                IsNotNullOrEmpty(destiny, "Destino", "O local de destino nao pode ser vazio").
                IsNotNullOrEmpty(date, "Data", "A data do voo nao pode ser vazia").
                IsTrue(isNotValidFormatDate, "Data", "A data informada nao e uma data valida").
                // IsFalse(isNotValidFormatDate, "Data", "A data informada não pode ser menor que a data atual").
                IsNotNullOrEmpty(departureTime, "Horario de saida", "O horario de saida nao pode ser vazio").
                IsTrue(isNotValidDepartureTime, "Horario de saida", "O horario de saida informado nao segue a estrutura de um horario valida").
                IsNotNullOrEmpty(arrivalTime, "Horario de chegada", "O horario de chegada nao pode ser vazio").
                IsTrue(isNotValidArrivalTime, "Horario de chegada", "O horario de chegada informado nao segue a estrutura de um horario valida");
            
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
    }
}