using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;


namespace BuyTickets.views
{
    public class FlightControllerView
    {
        private FlightController _flightController;

        public FlightControllerView(FlightController flightController)
        {
            _flightController = flightController;
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
            Flight flight = new Flight(origin, destiny, date, departureTime, arrivalTime, enterprise);
            
            var result = _flightController.Create(flight);

            if (result == null)
            {
                Console.WriteLine("O voo não foi criado...");
            }
            else
            {
                Console.WriteLine($"Voo {flight.Id} criado com sucesso!!!");
            }
        }

        public void SearchAll()
        {
            var flightListResult = _flightController.SearchAll();

            foreach (var flight in flightListResult)
            {
                Console.WriteLine($"Codigo Empresa: {flight.Enterprise.Id}; Empresa: {flight.Enterprise.Name};" +
                                    $"Origem: {flight.Origin}; Destino: {flight.Destiny}" +
                                    $"\nData: {flight.Date}; Saida: {flight.DepartureTime}; Chegada: {flight.ArrivalTime};" + 
                                    $"\nCodigo do Voo: {flight.Id}\n");
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
                flightResult.Origin = Console.ReadLine();
                Console.WriteLine("Informe o local de destino do voo: ");
                flightResult.Destiny = Console.ReadLine();
                Console.WriteLine("Informe a data do voo: ");
                flightResult.Date = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Informe a hora de saida do voo: ");
                flightResult.DepartureTime = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Informe a hora de chegada do voo: ");
                flightResult.ArrivalTime = DateTime.Parse(Console.ReadLine());
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