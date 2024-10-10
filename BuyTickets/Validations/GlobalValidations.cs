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
        public void CreateEnterpriseValidate(string name, string email, string password)
        {
            var contract = new Contract<Notification>().
                Requires().
                IsNotNull(name, "Nome", "O nome nao pode ser vazio").
                IsNotNull(email, "Email", "O email nao pode ser vazio").
                IsEmail(email, "Email", "O email informado nao e valido").
                IsNotNull(password, "Senha", "A senha nao pode ser vazia");
                
            AddNotifications(contract);
        }

        public void CreateFlightValidate(string origin, string destiny, DateTime date, DateTime departureTime, DateTime arrivalTime)
        {
            var contract = new Contract<Notification>().
                Requires().
                IsNotNull(origin, "Origem", "O local de origem nao pode ser vazio").
                IsNotNull(origin, "Destino", "O local de destino nao pode ser vazio").
                IsNotNull(origin, "Data", "A data do voo nao pode ser vazia").
                IsNotNull(origin, "Horario de saida", "O horario de saida nao pode ser vazio").
                IsNotNull(origin, "Horario de chegada", "O horario de chegada nao pode ser vazio");
        }
    }
}