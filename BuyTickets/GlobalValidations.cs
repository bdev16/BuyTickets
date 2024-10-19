using System;
using System.Collections.Generic;
using System.Globalization;
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
            //Cria uma variavel do tipo DateTime que vai servir como uma referencia para verificar se os dados informados pelos
            //usuarios vão estar de acordo com o tipo DateTime
            DateTime dateFormat, departureTimeFormat, arrivalTimeFormat;
            //Vai permitir que a conversão de Cultura de lingua seja feita através de valores informados dentro do codigo, de maneira automatica
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;

            var departureTimeWithDateInclude = $"{date} {departureTime}";
            var arrivalTimeWithDateInclude = $"{date} {arrivalTime}";
            
            //Os codigos abaixo tentam fazer a conversão dos valores que vão ser utilizados para o tipo datetime
            //O tryparseexact da classe datetime permite tentar realizar a conversão de maneira mais detalhada e evitando o lançamento de exceções
            //Se a conversão for possivel as variaveis receberam o valor FALSE, se forem possiveis de serem feitas retornaram valores TRUE
            bool isValidDate = DateTime.TryParseExact(date,"dd/MM/yyyy", cultureInfo, DateTimeStyles.None, out dateFormat);
            bool isValidDepartureTime = DateTime.TryParseExact(departureTimeWithDateInclude, "dd/MM/yyyy HH:mm", cultureInfo, DateTimeStyles.None, out departureTimeFormat);
            bool isValidArrivalTime = DateTime.TryParseExact(arrivalTimeWithDateInclude, "dd/MM/yyyy HH:mm", cultureInfo, DateTimeStyles.None, out arrivalTimeFormat);
            // var contract = new Contract<Notification>().
            var contract = new Contract<Notification>().
                Requires().
                IsNotNullOrEmpty(origin, "Origem", "O local de origem nao pode ser vazio").
                IsNotNullOrEmpty(destiny, "Destino", "O local de destino nao pode ser vazio").
                IsNotNullOrEmpty(date, "Data", "A data do voo nao pode ser vazia").
                IsTrue(isValidDate, "Data", "A data informada nao e uma data valida").
                IsNotNullOrEmpty(departureTime, "Horario de saida", "O horario de saida nao pode ser vazio").
                IsTrue(isValidDepartureTime, "Horario de saida", "O horario de saida informado nao segue a estrutura de um horario valida").
                IsNotNullOrEmpty(arrivalTime, "Horario de chegada", "O horario de chegada nao pode ser vazio").
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

        public NotificationResult CreateEnterpriseValidate(string name, string email, string password)
        {
            Clear();
            var contract = new Contract<Notification>().
                Requires().
                IsNotNullOrEmpty(name, "Nome", "O nome nao pode ser vazio").
                IsNotNullOrEmpty(email, "Email", "O email nao pode ser vazio").
                IsEmail(email, "Email", "O email informado nao e valido").
                IsNotNullOrEmpty(password, "Senha", "A senha nao pode ser vazia");
                
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