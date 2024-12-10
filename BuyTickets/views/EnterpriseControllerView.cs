using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;
using Flunt.Notifications;

namespace BuyTickets.views
{
    public class EnterpriseControllerView
    {
        private EnterpriseController _enterpriseController;
        private GlobalValidations _globalValidations;

        public EnterpriseControllerView(EnterpriseController enterpriseController, GlobalValidations globalValidations)
        { 
            _enterpriseController = enterpriseController;
            _globalValidations = globalValidations;
        }

        public void Create()
        {
            Console.WriteLine("Informe o nome da empresa: ");
            var name = Console.ReadLine();
            Console.WriteLine("Informe o email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Informe a senha");
            var password = Console.ReadLine();
            Console.WriteLine("Informe o cnpj");
            var cnpj = Console.ReadLine();

             //Utiliza o método CreateFlightValidations da classeglobalValidations para verificar se os dados informados pelo o usuarios são validos
            var resultValidations = _globalValidations.CreateEnterpriseValidate(name, email, password, cnpj);

            //Tenta converter o atributo Data da classe NotificationResult para um Lista de notificações
            //Se a conversão for feita com sucesso vai ser retornada uma lista se não vai ser retornado um null
            var listNotification = resultValidations.Data as IEnumerable<Notification>;
            
            if (resultValidations.Success == false)
            {
                Console.Clear();
                Console.WriteLine("Erros: ");
                if (listNotification != null)
                {
                    foreach(var result in listNotification)
                    {
                        Console.WriteLine($"    - {result.Message}");
                    }
                }
                Console.ReadKey();
            }
            else
            {
                Enterprise enterprise = new Enterprise(name, email, password, cnpj);

                var result = _enterpriseController.Create(enterprise);

                if (result == null)
                {
                    Console.WriteLine("Ocorreu um erro no cadastro da empresa...");
                }
                else
                {
                    Console.WriteLine($"Empresa {result.Id} cadastrada com sucesso!!!");
                }
            }
        }

        public void SearchAll()
        {
            var enterpriseListResult = _enterpriseController.SearchAll();

            if (enterpriseListResult == null)
            {
                Console.WriteLine("Nenhuma empresa foi cadastrada até o momento...");
            }
            else
            {
                foreach (var enterprise in enterpriseListResult)
                {
                    Console.WriteLine($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};\n");
                }
            }
        }

        public void SearchById(Enterprise enterprise)
        {
            var enterpriseResult = _enterpriseController.SearchById(enterprise.Id);

            if (enterpriseResult == null)
            {
                Console.WriteLine("A empresa informada não foi encontrada...");
            }
            else
            {
                Console.WriteLine($"Nome: {enterpriseResult.FullName};\nEmail: {enterpriseResult.Email};\nSenha: {enterpriseResult.Password}");
            }
        }

        public void SearchAllFlights(Enterprise enterprise)
        {
            var enterpriseListResult = _enterpriseController.SearchAllFlights(enterprise);

            if (enterpriseListResult == null)
            {
                Console.WriteLine("Nenhum voo foi adquirido até o momento...");
            }
            else
            {
                foreach (var flight in enterpriseListResult)
                {
                    Console.WriteLine($"Codigo Empresa: {flight.Enterprise.Id}; Empresa: {flight.Enterprise.FullName};" +
                                        $"Origem: {flight.Origin}; Destino: {flight.Destiny}" +
                                        $"\nData: {flight.Date}; Saida: {flight.DepartureTime}; Chegada: {flight.ArrivalTime};" + 
                                        $"\nCodigo do Voo: {flight.Id}\n");
                }
            }
        }

        // Esse método não está fazendo sentido, nunca vai conter uma empresa com id errado, pq esse método recebe a copia da instancia da empresa que já realizou o login no sistema
        // Então esse método não faz sentido
        public void Update(Enterprise enterprise)
        {
            var enterpriseResult = _enterpriseController.SearchById(enterprise.Id);
            if (enterpriseResult == null)
            {
                Console.WriteLine("A empresa informada não foi encontrada...");
            }
            else
            {
                //O codigo abaixo, cria uma variavel de verificação que ao receber um valor qualquer do usuario
                //Caso o valor informado pelo usuario for vazio, o atributo em questão de empresa vai receber o valor atual dele sem altera-lo
                //Caso o valor informado por diferente de vazio, o atributo em questão vai receber o valor que foi informado pelo usuario, alterando o valor que estava anteriormente
                Console.WriteLine("Os dados informados vão modificar os dados existente de empresa...");
                // Console.ReadKey();
                Console.WriteLine("Informe o nome da empresa(ou deixe em branco para nulo): ");
                var name = Console.ReadLine();
                if (name == "")
                {
                    enterprise.FullName = enterprise.FullName;
                }
                else
                {
                    enterpriseResult.FullName = name;
                }
                Console.WriteLine("Informe o email(ou deixe em branco para nulo): ");
                var email = Console.ReadLine();
                if (email == "")
                {
                    enterprise.Email = enterprise.Email;
                }
                else
                {
                    enterprise.Email = email;
                }
                Console.WriteLine("Informe a senha(ou deixe em branco para nulo):");
                var password = Console.ReadLine();
                if (password == "")
                {
                    enterprise.Password = enterprise.Password;
                }
                else
                {
                    enterpriseResult.Password = password;
                }
                
                var enterpriseUpdateResult = _enterpriseController.Update(enterpriseResult);

                if (enterpriseUpdateResult == null)
                {
                    Console.WriteLine("Ocorreu um erro ao tentar modificar os dados da empresa...");
                }
                else
                {
                    Console.WriteLine($"Empresa {enterpriseResult.Id} foi modificada com sucesso!!!");
                }
            }     
        }

        public void Delete(Enterprise enterprise)
        {
            var enterpriseResult = _enterpriseController.SearchById(enterprise.Id);
            if (enterpriseResult == null)
            {
                Console.WriteLine("Empresa não encontrada...");
            }
            else
            {
                var resultDeleteEnterprise = _enterpriseController.Delete(enterpriseResult.Id);

                if (resultDeleteEnterprise == null)
                {
                    Console.WriteLine($"Ocorreu um erro ao tentar deletar o voo informado...");
                }
                else
                {
                    Console.WriteLine($"Empresa {enterpriseResult.Id} deletada com sucesso!!!");
                }
            }
        }

        public void Login(MenuView menuView)
        {
            Console.WriteLine("Informe o email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Informe a senha: ");
            var password = Console.ReadLine();
            var loginResult = _enterpriseController.Login(email, password);

            if (loginResult == null)
            {
                Console.WriteLine("Email ou senha invalidos...");
            }
            else
            {
                menuView.EnterpriseMenu(loginResult);
            }
        }
    }
}