using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;
using BuyTickets.Validations;


namespace BuyTickets.views
{
    public class FlightControllerView
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
            bool vooRegistred = false;
            while (vooRegistred != true)
            {
                Console.Clear();
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

                try
                {
                    Flight flight = new Flight(origin, destiny, date, departureTime, arrivalTime, enterprise);
                
                    var resultValidations = _globalValidations.CreateFlightValidate(origin, destiny, date, departureTime, arrivalTime);
                    
                    if (resultValidations.Success == false)
                    {
                        Console.WriteLine(JsonSerializer.Serialize(resultValidations));
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        var result = _flightController.Create(flight);
                        Console.WriteLine($"Voo {result.Id} cadastrado com sucesso!!!");
                        vooRegistred = true;
                        Console.ReadKey();
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Erro ao tentar criar o Voo: {ex.Message}\nTente novamente.");
                }  
            }
            
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
                    Console.WriteLine($"Codigo Empresa: {flight.Enterprise.Id}; Empresa: {flight.Enterprise.Name};" +
                                        $"Origem: {flight.Origin}; Destino: {flight.Destiny}" +
                                        $"\nData: {flight.Date}; Saida: {flight.DepartureTime}; Chegada: {flight.ArrivalTime};" + 
                                        $"\nCodigo do Voo: {flight.Id}\n");
                }
            }
        }

        public void SearchById()
        {
            Console.WriteLine("Informe o ID do voo desejado: ");
            var flightId = Guid.Parse(Console.ReadLine());
            var flightResult = _flightController.SearchById(flightId);
            if (flightResult == null)
            {
                Console.WriteLine("Voo não encontrado...");
            }
            else
            {
                Console.WriteLine($"Codigo Empresa: {flightResult.Enterprise.Id}; Empresa: {flightResult.Enterprise.Name};" +
                                    $"Origem: {flightResult.Origin}; Destino: {flightResult.Destiny}" +
                                    $"\nData: {flightResult.Date}; Saida: {flightResult.DepartureTime}; Chegada: {flightResult.ArrivalTime};" + 
                                    $"\nCodigo do Voo: {flightResult.Id}\n");
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
                    Console.WriteLine($"Codigo Empresa: {flight.Enterprise.Id}; Empresa: {flight.Enterprise.Name};" +
                                    $"Origem: {flight.Origin}; Destino: {flight.Destiny}" +  
                                    $"\nData: {flight.Date}; Saida: {flight.DepartureTime}; Chegada: {flight.ArrivalTime};" + 
                                    $"\nCodigo do Voo: {flight.Id}");
                }
            }
        }

        public void Update()
        {
            Console.WriteLine("Informe o ID do voo desejado: ");
            var flightId = Guid.Parse(Console.ReadLine());
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
                //Ela vai ser atribuida ao atributo do elemento pesquisado anterior, substituindo o valor anterior
                //Se ele não receber nenhum valor ela vai atribuir o valor atual do atributo ao atributo novamente
                //Não modificando o valor do mesmo
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

        public void Delete()
        {
            Console.WriteLine("Informe o ID do voo desejado: ");
            var flightId = Guid.Parse(Console.ReadLine());
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
    }
}