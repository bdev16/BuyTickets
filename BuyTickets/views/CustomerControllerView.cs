using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;
using Flunt.Notifications;

namespace BuyTickets.views
{
    public class CustomerControllerView
    {
        private CustomerController _customerController;
        private FlightController _flightController;
        private GlobalValidations _globalValidations;

        public CustomerControllerView(CustomerController customerController, FlightController flightController, GlobalValidations globalValidations)
        { 
            _customerController = customerController;
            _flightController = flightController;
            _globalValidations = globalValidations;
        }

        public void Create()
        {
            Console.WriteLine("Informe o seu nome: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Informe o seu sobrenome: ");
            var lastName = Console.ReadLine();
            Console.WriteLine("Informe o seu email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Informe a sua senha:");
            var password = Console.ReadLine();
            Console.WriteLine("Informe o seu CPF:");
            var cpf = Console.ReadLine();

             //Utiliza o método CreateFlightValidations da classeglobalValidations para verificar se os dados informados pelo o usuarios são validos
            var resultValidations = _globalValidations.CreateCustomerValidate(firstName, lastName, email, password, cpf);

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
                        Console.WriteLine($" - {result.Message}");
                    }
                }
                Console.ReadKey();
            }
            else
            {
                Customer customer = new Customer(firstName, lastName, email, password, cpf);

                var result = _customerController.Create(customer);

                if (result == null)
                {
                    Console.WriteLine("Ocorreu um erro no cadastro de cliente...");
                }
                else
                {
                    Console.WriteLine($"Cliente {result.Id} cadastrado com sucesso!!!");
                }
            }
        }

        public void SearchAllFlights(Customer customer)
        {
            var customerListResult = _customerController.SearchAllFlights(customer);

            if (customerListResult == null)
            {
                Console.WriteLine("Nenhum voo foi adquirido até o momento...");
            }
            else
            {
                foreach (var flight in customerListResult)
                {
                    Console.WriteLine($"Codigo Empresa: {flight.Enterprise.Id}; Empresa: {flight.Enterprise.FullName};" +
                                        $"Origem: {flight.Origin}; Destino: {flight.Destiny}" +
                                        $"\nData: {flight.Date}; Saida: {flight.DepartureTime}; Chegada: {flight.ArrivalTime};" + 
                                        $"\nCodigo do Voo: {flight.Id}\n");
                }
            }
        }

        public void SearchById(Customer customer)
        {
            var customerResult = _customerController.SearchById(customer.Id);

            if (customerResult == null)
            {
                Console.WriteLine("O cliente informado não foi encontrado...");
            }
            else
            {
                Console.WriteLine($"Nome Completo: {customerResult.FullName};\nNome: {customerResult.FirstName};\nSobrenome: {customerResult.LastName};\nEmail: {customerResult.Email};\nSenha: {customerResult.Password}");
            }
        }

        public void Update(Customer customer)
        {
            var customerResult = _customerController.SearchById(customer.Id);
            if (customerResult == null)
            {
                Console.WriteLine("O cliente informado não foi encontrado...");
            }
            else
            {
                //O codigo abaixo, cria uma variavel de verificação que ao receber um valor qualquer do usuario
                //Caso o valor informado pelo usuario for vazio, o atributo em questão de empresa vai receber o valor atual dele sem altera-lo
                //Caso o valor informado por diferente de vazio, o atributo em questão vai receber o valor que foi informado pelo usuario, alterando o valor que estava anteriormente
                Console.WriteLine("Os dados informados vão modificar os dados existente de cliente...");
                Console.ReadKey();
                Console.WriteLine("Informe o seu nome(ou deixe em branco para nulo): ");
                var firstName = Console.ReadLine();
                if (firstName == "")
                {
                    customer.FirstName = customer.FirstName;
                }
                else
                {
                    customerResult.FirstName = firstName!;
                }
                Console.WriteLine("Informe o seu sobrenome(ou deixe em branco para nulo): ");
                var lastName = Console.ReadLine();
                if (lastName == "")
                {
                    customer.LastName = customer.LastName;
                }
                else
                {
                    customerResult.LastName = lastName!;
                }
                Console.WriteLine("Informe o email(ou deixe em branco para nulo): ");
                var email = Console.ReadLine();
                if (email == "")
                {
                    customerResult.Email = customerResult.Email;
                }
                else
                {
                    customerResult.Email = email!;
                }
                Console.WriteLine("Informe a senha(ou deixe em branco para nulo):");
                var password = Console.ReadLine();
                if (password == "")
                {
                    customerResult.Password = customerResult.Password;
                }
                else
                {
                    customerResult.Password = password!;
                }
                
                var customerUpdateResult = _customerController.Update(customerResult);

                if (customerUpdateResult == null)
                {
                    Console.WriteLine("Ocorreu um erro ao tentar modificar os dados de cliente...");
                }
                else
                {
                    Console.WriteLine($"Cliente {customerResult.Id} foi modificado com sucesso!!!");
                }
            }     
        }

        public void Delete(Customer customer)
        {
            var customerResult = _customerController.SearchById(customer.Id);
            if (customerResult == null)
            {
                Console.WriteLine("O cliente informado não foi encontrado...");
            }
            else
            {
                var resultDeleteCustomer = _customerController.Delete(customerResult.Id);

                if (!resultDeleteCustomer)
                {
                    Console.WriteLine($"Ocorreu um erro ao tentar deletar o cliente informado...");
                }
                else
                {
                    Console.WriteLine($"Cliente {customerResult.Id} deletado com sucesso!!!");
                }
            }
        }

        public void Login(MenuView menuView)
        {
            Console.WriteLine("Informe o email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Informe a senha: ");
            var password = Console.ReadLine();
            var loginResult = _customerController.Login(email!, password!);

            if (loginResult == null)
            {
                Console.WriteLine("Email ou senha invalidos...");
            }
            else
            {
                menuView.CustomerMenu(loginResult);
            }
        }

        public void BuyFlight(Customer customer)
        {
            try
            {
                Console.WriteLine("Informe o ID do Voo: ");
                var idInformed = Console.ReadLine();
                if (!Guid.TryParse(idInformed, out Guid id))
                {
                    throw new Exception();
                }
                else
                {
                    var idFlight = Guid.Parse(idInformed);
                    var resultSearchById = _flightController.SearchById(idFlight);
                    if (resultSearchById == null)
                    {
                        Console.WriteLine("O Voo informado não existe no sistema...");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Informe o seu nome: ");
                        var name = Console.ReadLine();
                        Console.WriteLine("Informe o seu sobrenome: ");
                        var lastName = Console.ReadLine();
                        Console.WriteLine("Informe seu CPF: ");
                        var cpf = Console.ReadLine();

                        bool confirmBuy = false;
                        Console.Clear();
                        Console.WriteLine("Deseja confirmar a compra?([1]Sim; [2]Não;)");
                        var option = Console.ReadLine();
                        switch (option)
                        {
                            case "1":
                                confirmBuy = true;
                                Console.WriteLine("Compra efetuada com sucesso!!!");
                                customer.purchasedFlights.Add(resultSearchById);
                                resultSearchById.registeredCustomers.Add(customer);
                                _flightController.Update(resultSearchById);
                                Console.ReadLine();
                                break;
                            case "2":
                                confirmBuy = false;
                                Console.WriteLine("Compra cancelada com sucesso!!!");
                                Console.ReadLine();
                                break;
                            default:
                                Console.Write($"O valor informado [{option}] não está entre as opções disponiveis...");
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Erro: O valor passado não é um ID valido...");
            }
        }
    }
}