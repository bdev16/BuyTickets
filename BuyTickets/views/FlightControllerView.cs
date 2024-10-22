using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;
using Flunt.Notifications;


namespace BuyTickets.views
{
    public class FlightControllerView : Notifiable<Notification>
    {
        private FlightController _flightController;
        private GlobalValidations _globalValidations;

        public FlightControllerView(FlightController flightController, GlobalValidations globalValidations)
        {
            _flightController = flightController;
            _globalValidations = globalValidations;
        }

        public void Create(Enterprise enterprise)
        {
            Console.WriteLine("Informe o local de origem do voo: ");            
            var origin = Console.ReadLine();
            Console.WriteLine("Informe o local de destino do voo: ");  
            var destiny = Console.ReadLine();
            Console.WriteLine("Informe a data do voo: ");
            var date = Console.ReadLine();
            Console.WriteLine("Informe a hora de saida do voo: ");
            var departureTime = Console.ReadLine();
            Console.WriteLine("Informe a hora de chegada do voo: ");
            var arrivalTime = Console.ReadLine();

            //Utiliza o método CreateFlightValidations da classeglobalValidations para verificar se os dados informados pelo o usuarios são validos
            var resultValidations = _globalValidations.CreateFlightValidate(origin, destiny, date, departureTime, arrivalTime);

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
                Console.Clear();
                Flight flight = new Flight(origin, destiny, date, departureTime, arrivalTime, enterprise);
                var result = _flightController.Create(flight);
                enterprise.Flights.Add(flight);
                Console.WriteLine($"Voo {result.Id} cadastrado com sucesso!!!");
                Console.ReadKey();
            }

            // Flight flight = new Flight(origin, destiny, date, departureTime, arrivalTime, enterprise);
            
            // var result = _flightController.Create(flight);

            // if (result == null)
            // {
            //     Console.WriteLine("O voo não foi criado...");
            // }
            // else
            // {
            //     Console.WriteLine($"Voo {flight.Id} criado com sucesso!!!");
            // }
        }

        public void SearchAll()
        {
            var flightListResult = _flightController.SearchAll();

            if (flightListResult == null)
            {
                Console.WriteLine("Nenhum voo foi cadastrado até o momento...");
            }
            else
            {
                foreach (var flight in flightListResult)
                {
                    Console.WriteLine($"Codigo Empresa: {flight.Enterprise.Id}; Empresa: {flight.Enterprise.FullName};" +
                                        $"Origem: {flight.Origin}; Destino: {flight.Destiny}" +
                                        $"\nData: {flight.Date}; Saida: {flight.DepartureTime}; Chegada: {flight.ArrivalTime};" + 
                                        $"\nCodigo do Voo: {flight.Id}\n");
                }
            }
        }

        public void SearchById()
        {
            var flightInformed = "";
            try
            {
                Guid guidFormat;
                var flightId = new Guid();
                Console.WriteLine("Informe o ID do voo desejado: ");
                flightInformed = Console.ReadLine();
                if (Guid.TryParse(flightInformed, out guidFormat))
                {
                    flightId = Guid.Parse(flightInformed);
                } 
                else
                {
                    throw new Exception();
                }

                var flightResult = _flightController.SearchById(flightId);

                if (flightResult == null)
                {
                    Console.WriteLine("Voo não encontrado...");
                }
                else
                {
                    Console.WriteLine($"Codigo Empresa: {flightResult.Enterprise.Id}; Empresa: {flightResult.Enterprise.FullName};" +
                                        $"Origem: {flightResult.Origin}; Destino: {flightResult.Destiny}" +
                                        $"\nData: {flightResult.Date}; Saida: {flightResult.DepartureTime}; Chegada: {flightResult.ArrivalTime};" + 
                                        $"\nCodigo do Voo: {flightResult.Id}\n");
                }
            }
            catch (Exception)
            {
                if (flightInformed == "")
                {
                    Console.Clear();
                    Console.WriteLine("Erro: O ID informado não pode ser um valor vazio...");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Erro: O valor do ID informado não corresponde ao padrão existente...");
                }
            }   
        }

        public void SearchByEnterprise(Enterprise enterprise)
        {
            var listEnterpriseFlightsResult = _flightController.SearchByEnterprise(enterprise);

            if (listEnterpriseFlightsResult == null)
            {
                Console.WriteLine("Sem voos cadastrados no momento");
            }
            else
            {
                foreach (var flight in listEnterpriseFlightsResult)
                {
                    Console.WriteLine($"Codigo Empresa: {flight.Enterprise.Id}; Empresa: {flight.Enterprise.FullName};" +
                                    $"Origem: {flight.Origin}; Destino: {flight.Destiny}" +  
                                    $"\nData: {flight.Date}; Saida: {flight.DepartureTime}; Chegada: {flight.ArrivalTime};" + 
                                    $"\nCodigo do Voo: {flight.Id}");
                }
            }
        }

        public void Update()
        {
            //A variavel flightInformad está sendo criada fora do escopo do Try para poder ser utilizada pelo Catch
            //Podendo assim gerar uma mensagem de erro especifica caso ela esteja vazia ""
            var flightInformed = "";
            try
            {
                //A variavel guidFormat está sendo criada para ser utilizada na estrutura condicional logo abaixo como uma varivel base
                Guid guidFormat;
                var flightId = new Guid();
                Console.WriteLine("Informe o ID do voo desejado: ");
                flightInformed = Console.ReadLine();
                //Essa estrutura condicional verifica se o valor informado pelo usuario presente na variavle flightInformad está de acordo com
                //O padrão de um Guid, se estiver o valor vai ser convertido normalmente para o tipo Guid, sendo atribuido pela variavel flightId
                //Se não estiver de acordo com o padrão Guid, será gerada uma exceção do tipo Format
                if (Guid.TryParse(flightInformed, out guidFormat))
                {
                    flightId = Guid.Parse(flightInformed);
                }
                else
                {
                    throw new Exception();
                }

                var flightResult = _flightController.SearchById(flightId);

                if (flightResult == null)
                {
                    Console.WriteLine("Voo não encontrado...");
                }
                else
                {
                    Console.WriteLine("Os dados informados vão modificar os dados do voo existente.");
                    Console.WriteLine("Informe o local de origem do voo: ");
                    //O codigo abaixo, cria uma variavel de verificação que ao receber um valor qualquer do usuario
                    //Caso o valor informado pelo usuario for vazio, o atributo em questão de empresa vai receber o valor atual dele sem altera-lo
                    //Caso o valor informado por diferente de vazio, o atributo em questão vai receber o valor que foi informado pelo usuario, alterando o valor que estava anteriormente
                    var origin = Console.ReadLine();
                    if (origin == "")
                    {
                        flightResult.Origin = flightResult.Origin;
                    }
                    else
                    {
                        flightResult.Origin = origin!;
                    }
                    Console.WriteLine("Informe o local de destino do voo: ");
                    var destiny = Console.ReadLine();
                    if (destiny == "")
                    {
                        flightResult.Destiny = flightResult.Destiny;
                    }
                    else
                    {
                        flightResult.Destiny= destiny!;
                    }
                    Console.WriteLine("Informe a data do voo: ");
                    var date = Console.ReadLine();
                    if (date == "")
                    {
                        flightResult.Date = flightResult.Date;
                    }
                    else
                    {
                        flightResult.Date = DateTime.Parse(date!);
                    }
                    Console.WriteLine("Informe a hora de saida do voo: ");
                    var departureTime = Console.ReadLine();
                    if (date == "")
                    {
                        flightResult.DepartureTime = flightResult.DepartureTime;
                    }
                    else
                    {
                        flightResult.DepartureTime = DateTime.Parse(departureTime!);
                    }
                    Console.WriteLine("Informe a hora de chegada do voo: ");
                    var arrivalTime = Console.ReadLine();
                    if (date == "")
                    {
                        flightResult.ArrivalTime = flightResult.ArrivalTime;
                    }
                    else
                    {
                        flightResult.ArrivalTime = DateTime.Parse(arrivalTime!);
                    }
                    var resultUpdateFlight = _flightController.Update(flightResult);
                    
                    if (resultUpdateFlight == null)
                    {
                        Console.WriteLine("Ocorreu um erro ao realizar as modificações");
                    }
                    else
                    {
                        Console.WriteLine($"Voo {resultUpdateFlight.Id} foi modificado com sucesso!!!");
                    }
                }
            }
            catch (Exception)
            {
                if (flightInformed == "")
                {
                    Console.Clear();
                    Console.WriteLine("Erro: O ID informado não pode ser um valor vazio...");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Erro: O valor do ID informado não corresponde ao padrão existente...");
                }
            }
        }

        public void Delete()
        {
            //A variavel flightInformad está sendo criada fora do escopo do Try para poder ser utilizada pelo Catch
            //Podendo assim gerar uma mensagem de erro especifica caso ela esteja vazia ""
            var flightInformed = "";
            try
            {
                //A variavel guidFormat está sendo criada para ser utilizada na estrutura condicional logo abaixo como uma varivel base
                Guid guidFormat;
                var flightId = new Guid();
                Console.WriteLine("Informe o ID do voo desejado: ");
                flightInformed = Console.ReadLine();
                //Essa estrutura condicional verifica se o valor informado pelo usuario presente na variavle flightInformad está de acordo com
                //O padrão de um Guid, se estiver o valor vai ser convertido normalmente para o tipo Guid, sendo atribuido pela variavel flightId
                //Se não estiver de acordo com o padrão Guid, será gerada uma exceção do tipo Format
                if (Guid.TryParse(flightInformed, out guidFormat))
                {
                    flightId = Guid.Parse(flightInformed);
                }
                else
                {
                    throw new Exception();
                }

                var flightResult = _flightController.SearchById(flightId);

                if (flightResult == null)
                {
                    Console.WriteLine("Voo não encontrado...");
                }
                else
                {
                    var resultDeleteFlight = _flightController.Delete(flightResult.Id);

                    if (resultDeleteFlight == null)
                    {
                        Console.WriteLine($"Ocorreu um erro ao tentar deletar o voo informado...");
                    }
                    else
                    {
                        Console.WriteLine($"Voo {flightResult.Id} deletado com sucesso!!!");
                    }
                }
            }
            catch (Exception)
            {
                if (flightInformed == "")
                {
                    Console.Clear();
                    Console.WriteLine("Erro: O ID informado não pode ser um valor vazio...");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Erro: O valor do ID informado não corresponde ao padrão existente...");
                }
            }
        }
    }
}