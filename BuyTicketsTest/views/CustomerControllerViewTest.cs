using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.models;
using Xunit.Abstractions;

namespace BuyTicketsTest.views
{
    public class CustomerControllerViewTest : IClassFixture<FixtureControllersAndViews>
    {
        
        private readonly FixtureControllersAndViews _fixture;
        private readonly ITestOutputHelper _output;

        public CustomerControllerViewTest(FixtureControllersAndViews fixture, ITestOutputHelper output)
        {
            _output = output;
            _fixture = fixture;
        }

        [Fact]  
        public void Create_ShouldCreateCustomer_WhenTheInputsIsValid()
        {
            // Arrange

            var customerController = _fixture.CustomerController;
            var customerControllerView = _fixture.CustomerControllerView;

            //Act

            using (var input = new StringReader("Robson\nBambu\nrobsonbambu@gmail.com\nrobson123\n50460930420"))
            using (var output = new StringWriter())
            {
                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);
                
                customerControllerView.Create();

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var linesForIdCustomer = linesToConsoleOutput[5];

                // Extraindo o GUID
                var idCustomer = linesForIdCustomer.Substring(8, 36); // GUID tem 36 caracteres no formato padrão.

                // Buscando o usuario cliente pelo ID
                var resultCreateCustomer = customerController.SearchById(Guid.Parse(idCustomer));

                // //Excluindo o usuario cliente criado
                customerController.Delete(Guid.Parse(idCustomer));

                // //Verificando se o usuario cliente foi excluido
                var resultDeleteCustomer = customerController.SearchById(Guid.Parse(idCustomer));

                Assert.Equal($"Cliente {idCustomer} cadastrado com sucesso!!!", linesForIdCustomer);
                Assert.NotNull(resultCreateCustomer);
                Assert.Equal(null, resultDeleteCustomer);
            }
        }

        [Fact]
        public void SearchAllFlights_ShouldReturnMessageNoFlightsExist()
        {
            // Given

            var customer = _fixture.Customer;
            var customerController = _fixture.CustomerController;
            var customerControllerView = _fixture.CustomerControllerView;

            // When
        
            using (var output = new StringWriter())
            {
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                // Act
                customerControllerView.SearchAllFlights(customer);

                // Assert
                var outputResult = output.ToString();
                _output.WriteLine($"{outputResult}");

                Assert.Equal($"Nenhum voo foi adquirido até o momento...{Environment.NewLine}", outputResult);
            }
        }

        [Fact]
        public void SearchAllFlights_ShouldReturnAllFlightsAcquired()
        {
            // Given

            var customer = _fixture.Customer;
            var enterprise = _fixture.Enterprise;
            var flight = _fixture.Flights[0];
            var customerControllerView = _fixture.CustomerControllerView;
            var flightControllerView = _fixture.FlightControllerView;
            string idFlight;
            
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

                idFlight = lineToIdFlight.Substring(15, 36);

                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"\nData: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"\nCodigo do Voo: {idFlight}\n{Environment.NewLine}", outputResult);
            }

            using (var input = new StringReader($"{idFlight}\nJoelinton\nSouza\n50460930420\n1"))
            using (var output = new StringWriter())
            {

                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                customerControllerView.BuyFlight(customer);

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var lineToMessage = linesToConsoleOutput[5];

                Assert.Equal("Compra efetuada com sucesso!!!", lineToMessage);
            }

            using (var output = new StringWriter())
            {
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                // Act
                customerControllerView.SearchAllFlights(customer);

                // Assert
                var outputResult = output.ToString();
                _output.WriteLine($"{outputResult}");

                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {flight.Enterprise.FullName};" +
                                        $"Origem: {flight.Origin}; Destino: {flight.Destiny}" +
                                        $"\nData: {flight.Date}; Saida: {flight.DepartureTime}; Chegada: {flight.ArrivalTime};" + 
                                        $"\nCodigo do Voo: {flight.Id}\n{Environment.NewLine}", outputResult);
            }
        }

        [Fact]
        public void BuyFlight_ShouldReturnMessageSuccessBuy()
        {
            // Given

            var customer = _fixture.Customer;
            var enterprise = _fixture.Enterprise;
            var customerControllerView = _fixture.CustomerControllerView;
            var flightControllerView = _fixture.FlightControllerView;
            string idFlight;
            
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

                idFlight = lineToIdFlight.Substring(15, 36);

                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"\nData: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"\nCodigo do Voo: {idFlight}\n{Environment.NewLine}", outputResult);
            }

            using (var input = new StringReader($"{idFlight}\nJoelinton\nSouza\n50460930420\n1"))
            using (var output = new StringWriter())
            {

                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                customerControllerView.BuyFlight(customer);

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var lineToMessage = linesToConsoleOutput[5];

                Assert.Equal("Compra efetuada com sucesso!!!", lineToMessage);
            }
        }

        [Fact]
        public void BuyFlight_ShouldReturnMessageCancelBuy()
        {
            // Given

            var customer = _fixture.Customer;
            var enterprise = _fixture.Enterprise;
            var customerControllerView = _fixture.CustomerControllerView;
            var flightControllerView = _fixture.FlightControllerView;
            string idFlight;

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

                idFlight = lineToIdFlight.Substring(15, 36);

                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"\nData: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"\nCodigo do Voo: {idFlight}\n{Environment.NewLine}", outputResult);
            }

            using (var input = new StringReader($"{idFlight}\nJoelinton\nSouza\n50460930420\n2"))
            using (var output = new StringWriter())
            {

                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                customerControllerView.BuyFlight(customer);

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var lineToMessage = linesToConsoleOutput[5];

                Assert.Equal("Compra cancelada com sucesso!!!", lineToMessage);
            }
        }

        [Fact]
        public void BuyFlight_ShouldReturnMessageOptionNonexistent()
        {
            // Given

            var customer = _fixture.Customer;
            var enterprise = _fixture.Enterprise;
            var customerControllerView = _fixture.CustomerControllerView;
            var flightControllerView = _fixture.FlightControllerView;
            string idFlight;

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

                idFlight = lineToIdFlight.Substring(15, 36);

                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: RIO BRANCO (AC); Destino: MACAPA (AP)" +
                                        $"\nData: 30/12/2024 00:00:00; Saida: 30/12/2024 08:00:00; Chegada: 30/12/2024 10:00:00;" + 
                                        $"\nCodigo do Voo: {idFlight}\n{Environment.NewLine}", outputResult);
            }

            using (var input = new StringReader($"{idFlight}\nJoelinton\nSouza\n50460930420\nVaiDarErro"))
            using (var output = new StringWriter())
            {

                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                customerControllerView.BuyFlight(customer);

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var lineToMessage = linesToConsoleOutput[5];

                Assert.Equal("O valor informado [VaiDarErro] não está entre as opções disponiveis...", lineToMessage);
            }
        }

        [Fact]
        public void BuyFlight_ShouldReturnMessageToFlightNonexistent()
        {
            // Given

            var customer = _fixture.Customer;
            var customerControllerView = _fixture.CustomerControllerView;
            string idFlight;


            using (var input = new StringReader($"3f5d8a90-4a92-4a75-9f92-fc2bfc8b2a5f\nJoelinton\nSouza\n50460930420\nVaiDarErro"))
            using (var output = new StringWriter())
            {

                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                customerControllerView.BuyFlight(customer);

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var lineToMessage = linesToConsoleOutput[1];

                Assert.Equal("O Voo informado não existe no sistema...", lineToMessage);
            }
        }

        [Fact]
        public void BuyFlight_ShouldReturnMessageToIdFlightToFormatIncorrect()
        {
            // Given

            var customer = _fixture.Customer;
            var customerControllerView = _fixture.CustomerControllerView;
            string idFlight;

            using (var input = new StringReader($"incorreto\nJoelinton\nSouza\n50460930420\nVaiDarErro"))
            using (var output = new StringWriter())
            {

                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                customerControllerView.BuyFlight(customer);

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var lineToMessage = linesToConsoleOutput[1];

                Assert.Equal("Erro: O valor passado não é um ID valido...", lineToMessage);
            }
        }

        [Fact]
        public void SearchById_ShouldReturnMessageWithCustomerData()
        {
            //Arrange

            var customer = _fixture.Customer;
            var customerControllerView = _fixture.CustomerControllerView;
            
            //Act

            using (var input = new StringReader($"{customer.Id}\n2"))
            using (var output = new StringWriter())
            {
                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                customerControllerView.SearchById(customer);

                var consoleOutputResult = output.ToString();

                //Assert

                Assert.Equal($"Nome Completo: {customer.FullName};\nNome: {customer.FirstName};\nSobrenome: {customer.LastName};\nEmail: {customer.Email};\nSenha: {customer.Password}{Environment.NewLine}", consoleOutputResult);
            }
        }

        [Fact]
        public void Update_ShouldModifyCustomerInformed_And_ReturnSucessPhrase()
        {

            //Arrange

            var customerController = _fixture.CustomerController;
            var customerControllerView = _fixture.CustomerControllerView;
            string idCustomer;
            Customer createdCustomer;

            //Act

            using (var input = new StringReader("Elielson\nJustiça\nelielson@gmail.com\n123\n53575915420"))
            using (var output = new StringWriter())
            {
                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);
                
                customerControllerView.Create();

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var linesForIdCustomer = linesToConsoleOutput[5];

                // Extraindo o GUID
                idCustomer = linesForIdCustomer.Substring(8, 36); // GUID tem 36 caracteres no formato padrão.

                // Buscando o usuario cliente pelo ID
                createdCustomer = customerController.SearchById(Guid.Parse(idCustomer));

                // // //Excluindo o usuario cliente criado
                // customerController.Delete(Guid.Parse(idCustomer));

                // // //Verificando se o usuario cliente foi excluido
                // var resultDeleteCustomer = customerController.SearchById(Guid.Parse(idCustomer));

                Assert.Equal($"Cliente {idCustomer} cadastrado com sucesso!!!", linesForIdCustomer);
            }

            using (var input = new StringReader($"{idCustomer}\nEdison\nElias\nedison@gmail.com\n1234"))
            using (var output = new StringWriter())
            {

                Console.SetIn(input);
                Console.SetOut(output);

                customerControllerView.Update(createdCustomer);

                var outputResult = output.ToString();

                var linesOutputResult = outputResult.Split('\n');

                var mensageSucessResult = linesOutputResult[5];

                //Excluindo o usuario cliente criado
                var result = customerController.Delete(Guid.Parse(idCustomer));

                Assert.Equal($"Cliente {idCustomer} foi modificado com sucesso!!!\r", mensageSucessResult);
                Assert.Equal(true, result);
            }
        }
    }
}