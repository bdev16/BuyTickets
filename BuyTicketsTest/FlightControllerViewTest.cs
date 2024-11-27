using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BuyTickets.models;
using BuyTickets.Controllers;
using System.ComponentModel.Design;
using BuyTickets.views;
using BuyTickets;
using BuyTicketsTest;
using Xunit.Abstractions;
using System.Text;

namespace BuyTicketsTest
{
    public class FlightControllerViewTest
    {  
        private readonly FlightControllerViewFixture _fixture;
        private readonly ITestOutputHelper _output;

        public FlightControllerViewTest(FlightControllerViewFixture fixture, ITestOutputHelper output)
        {
            _output = output;
            _fixture = fixture;
        }

         [Fact]
        public void Create_ShouldCreateFlight_WhenTheInputsIsValid()
        {
            // Arrange
            var enterprise = _fixture.Enterprise;
            var flightControllerView = _fixture.FlightControllerView;
            var flightController = _fixture.FlightController;

            //
            using (var input = new StringReader("manaus\nbelem\n30/12/2024\n08:00\n10:00"))
            using (var output = new StringWriter())
            {
                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                // Act
                flightControllerView.Create(enterprise);

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var linesForIdFlight = linesToConsoleOutput[5];

                // Extraindo o GUID
                var idFlight = linesForIdFlight.Substring(4, 36); // GUID tem 36 caracteres no formato padrão.

                // Buscando o voo pelo ID
                var resultCreateFlight = flightController.SearchById(Guid.Parse(idFlight));

                //Excluindo o Voo criado
                flightController.Delete(Guid.Parse(idFlight));

                //Verificando se o voo foi excluido
                var resultDeleteFlight = flightController.SearchById(Guid.Parse(idFlight));

                // Validando que o voo foi criado corretamente
                Assert.NotNull(resultCreateFlight);
                Assert.Equal(null, resultDeleteFlight);
            }
        }

        [Fact]
        public void SearchAll_ShouldReturnAllFlight_ThatRegistered()
        {
            // Arrange

            var enterprise = _fixture.Enterprise;
            var flightControllerView = _fixture.FlightControllerView;

            using (var output = new StringWriter())
            {
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                // Act
                flightControllerView.SearchAll();

                // Assert
                var outputResult = output.ToString();
                _output.WriteLine($"{outputResult}");

                var linesOutputResult = outputResult.Split('\n');

                var lineToIdFlight = linesOutputResult[2];

                var idFlight = lineToIdFlight.Substring(15, 36);

                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"\nData: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"\nCodigo do Voo: {idFlight}\n{Environment.NewLine}", outputResult);
            }
            // _output.WriteLine($"Número de voos na lista: {_fixture.Flights.Count}");
            // Assert.False(true, "Forçando falha para ver saída.");
        }

        [Fact]
        public void SearchById_ShouldReturnFlight_InformedByParameter_WithoutSeeingPassengers()
        {
             //Arrange

            var enterprise = _fixture.Enterprise;
            var flightControllerView = _fixture.FlightControllerView;
            var customerOrEnterprise = new Tuple<Customer, Enterprise>(null, enterprise);
            string idFlight;
            
            //Act

            using (var output = new StringWriter())
            {
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                flightControllerView.SearchAll();
                
                var outputResult = output.ToString();
                output.WriteLine($"{outputResult}");

                var linesOutputResult = outputResult.Split('\n');

                var lineToIdFlight = linesOutputResult[2];

                idFlight = lineToIdFlight.Substring(15, 36);

                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"\nData: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"\nCodigo do Voo: {idFlight}\n{Environment.NewLine}", outputResult);
            }

            using (var input = new StringReader($"{idFlight}\n2"))
            using (var output = new StringWriter())
            {
                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                flightControllerView.SearchById(customerOrEnterprise);

                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var dataToFlightInformed = linesToConsoleOutput[1];

                //Assert

                // Validando que o voo foi criado corretamente
                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"\nData: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"\nCodigo do Voo: {idFlight}\n", dataToFlightInformed);
            }
            // _output.WriteLine($"Número de voos na lista: {_fixture.Flights.Count}");
            // Assert.False(true, "Forçando falha para ver saída.");
        }

        [Fact]
        public void SearchById_ShouldReturnFlight_InformedByParameter_ShowingThePassengers()
        {
            // Arrange

            var enterprise = _fixture.Enterprise;
            var flightControllerView = _fixture.FlightControllerView;
            var customerOrEnterprise = new Tuple<Customer, Enterprise>(null, enterprise);
            string idFlight;
            
            // Act

            flightControllerView.SearchAll();

            // _output.WriteLine($"Número de voos na lista: {_fixture.Flights.Count}");
            // Assert.False(true, "Forçando falha para ver saída.");

            using (var output = new StringWriter())
            {
                // Muda o padrão de saida de dados
                Console.SetOut(output);

                flightControllerView.SearchAll();
                
                var outputResult = output.ToString();
                // _output.WriteLine($"{outputResult}");

                var linesOutputResult = outputResult.Split('\n');

                var lineToIdFlight = linesOutputResult[2];

                idFlight = lineToIdFlight.Substring(15, 36);
                // output.WriteLine($"{idFlight}");

                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"\nData: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"\nCodigo do Voo: {idFlight}\n{Environment.NewLine}", outputResult);
            }

            using (var input = new StringReader($"{idFlight}\n1"))
            using (var output = new StringWriter())
            {
                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                flightControllerView.SearchById(customerOrEnterprise);

                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var dataToFlightInformed = linesToConsoleOutput[1];

                //Obtendo a 4 linha do output onde está o resultado da busca por passageiros no voo
                var dataToPassagersAtTheFlight = linesToConsoleOutput[3];
                _output.WriteLine($"{dataToPassagersAtTheFlight}");

                //Assert

                // Validando que o voo foi criado corretamente
                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"\nData: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"\nCodigo do Voo: {idFlight}\n", dataToFlightInformed);

                Assert.Equal("Não existem clientes cadastrados a esse voo...", dataToPassagersAtTheFlight);
            }
        }

        [Fact]
        public void FlightFilter_ShouldReturnFlight_InformedAllParameters()
        { 
            //Arrange

            var enterprise = _fixture.Enterprise;
            var flightControllerView = _fixture.FlightControllerView;
            string idFlight;

            //Act

            using (var input = new StringReader($"RIO BRANCO (AC)\nMACAPA (AP)\n30/12/2024"))
            using (var output = new StringWriter())
            {
                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                flightControllerView.FlightFilter();

                var outputResult = output.ToString();
                _output.WriteLine($"{outputResult}");

                var linesOutputResult = outputResult.Split('\n');

                var lineWithIdFlight = linesOutputResult[5];

                idFlight = lineWithIdFlight.Substring(15, 36);

                StringBuilder resultFlightFilter = new StringBuilder();
                for(int i = 3; i <= 5; i++)
                {
                    resultFlightFilter.Append(linesOutputResult[i]);
                }

                _output.WriteLine($"{resultFlightFilter.ToString()}");

                // var flightListResult = new String[linesOutputResult.Length];
                
                // Array.Copy(linesOutputResult, 3, flightListResult, 3, linesOutputResult.Length);
                
                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"Data: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"Codigo do Voo: {idFlight}", resultFlightFilter.ToString());
            }
        }

    }
}