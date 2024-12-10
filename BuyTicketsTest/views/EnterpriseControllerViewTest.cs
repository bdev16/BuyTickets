using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;
using BuyTickets.views;
using Xunit.Abstractions;

namespace BuyTicketsTest.views
{
    public class EnterpriseControllerViewTest : IClassFixture<FixtureControllersAndViews>
    {
        private readonly FixtureControllersAndViews _fixture;
        private readonly ITestOutputHelper _output;

        public EnterpriseControllerViewTest(FixtureControllersAndViews fixture, ITestOutputHelper output)
        {
            _output = output;
            _fixture = fixture;
        }

        [Fact]  
        public void Create_ShouldCreateEnterprise_WhenTheInputsIsValid()
        {
            // Arrange
            
            var enterpriseController = _fixture.EnterpriseController;
            var enterpriseControllerView = _fixture.EnterpriseControllerView;

            //Act

            using (var input = new StringReader("gol\ngol.airlines@gmail.com\ngol123\n35062925638499"))
            using (var output = new StringWriter())
            {
                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);
                
                enterpriseControllerView.Create();

                // Assert
                var consoleOutputResult = output.ToString();

                // Dividindo saída em linhas
                var linesToConsoleOutput = consoleOutputResult.Split(Environment.NewLine);

                // Obtendo a 6ª linha onde está o ID do voo
                var linesForIdEnterprise = linesToConsoleOutput[4];
                _output.WriteLine($"{linesForIdEnterprise}");

                // Extraindo o GUID
                var idEnterprise = linesForIdEnterprise.Substring(8, 36);
                _output.WriteLine($"{idEnterprise}"); // GUID tem 36 caracteres no formato padrão.

                // Buscando a empresa pelo ID
                var resultCreateEnterprise = enterpriseController.SearchById(Guid.Parse(idEnterprise));

                // //Excluindo a empresa criada
                enterpriseController.Delete(Guid.Parse(idEnterprise));

                // //Verificando se a empresa foi excluida
                var resultDeleteEnterprise = enterpriseController.SearchById(Guid.Parse(idEnterprise));

                // // Validando que a empresa foi criada corretamente
                Assert.NotNull(resultCreateEnterprise);
                Assert.Equal(null, resultDeleteEnterprise);
                // Assert.Equal(2, 5);
            }
        }

        [Fact]
        public void SearchAll_ShouldReturnAllFlight_ThatRegistered()
        {
            // Arrange

            var enterpriseController = _fixture.EnterpriseController;
            var enterpriseControllerView = _fixture.EnterpriseControllerView;

            using (var output = new StringWriter())
            {
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                // Act
                enterpriseControllerView.SearchAll();

                // Assert
                var outputResult = output.ToString();
                _output.WriteLine($"{outputResult}");

                var idEnterprise = outputResult.Substring(16, 36);

                Assert.Equal($"Codigo Empresa: {idEnterprise}; Empresa: {_fixture.Enterprise.FullName};\n{Environment.NewLine}", outputResult);
            }
        }

        [Fact]
        public void SearchById_ShouldReturnFlight_InformedByParameter()
        {
            //Arrange

            var enterprise = _fixture.Enterprise;
            var enterpriseController = _fixture.EnterpriseController;
            var enterpriseControllerView = _fixture.EnterpriseControllerView;
            
            //Act

            using (var output = new StringWriter())
            {
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                // Act
                enterpriseControllerView.SearchAll();

                // Assert
                var outputResult = output.ToString();
                _output.WriteLine($"{outputResult}");

                var idEnterprise = outputResult.Substring(16, 36);

                Assert.Equal($"Codigo Empresa: {idEnterprise}; Empresa: {_fixture.Enterprise.FullName};\n{Environment.NewLine}", outputResult);
            }

            using (var input = new StringReader($"{enterprise.Id}\n2"))
            using (var output = new StringWriter())
            {
                //Muda o padrão de entrada de dados
                Console.SetIn(input);
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                enterpriseControllerView.SearchById(enterprise);

                var consoleOutputResult = output.ToString();

                //Assert

                // Validando que a empresa captura foi a informada
                Assert.Equal($"Nome: {enterprise.FullName};\nEmail: {enterprise.Email};\nSenha: {enterprise.Password}{Environment.NewLine}", consoleOutputResult);
            }
        }

        [Fact]
        public void SearchAllFlights_ShouldReturnMessageNoFlightsExist()
        {
            // Given

            var enterprise = _fixture.Enterprise;
            var enterpriseController = _fixture.EnterpriseController;
            var enterpriseControllerView = _fixture.EnterpriseControllerView;

            // When
        
            using (var output = new StringWriter())
            {
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                // Act
                enterpriseControllerView.SearchAllFlights(enterprise);

                // Assert
                var outputResult = output.ToString();
                _output.WriteLine($"{outputResult}");

                Assert.Equal($"Nenhum voo foi adquirido até o momento...{Environment.NewLine}", outputResult);
            }
        }

        [Fact]
        public void SearchAllFlights_ShouldReturnAllFlightsRegisteredToEnterprise()
        {
            // Given

            var enterprise = _fixture.Enterprise;
            var enterpriseController = _fixture.EnterpriseController;
            var enterpriseControllerView = _fixture.EnterpriseControllerView;
            var flightControllerView = _fixture.FlightControllerView;
            var flightController = _fixture.FlightController;
            Flight flightCreated;

            // When
        
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
                flightCreated = flightController.SearchById(Guid.Parse(idFlight));

                // Validando que o voo foi criado corretamente
                Assert.NotNull(flightCreated);
            }

            using (var output = new StringWriter())
            {
                //Muda o padrão de saida de dados
                Console.SetOut(output);

                // Act
                enterpriseControllerView.SearchAllFlights(enterprise);

                // Assert
                var outputResult = output.ToString();
                _output.WriteLine($"{outputResult}");

                //Excluindo o Voo criado
                flightController.Delete(flightCreated.Id);

                //Verificando se o voo foi excluido
                var resultDeleteFlight = flightController.SearchById(flightCreated.Id);

                Assert.Equal($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.FullName};" +
                                        $"Origem: {flightCreated.Origin}; Destino: {flightCreated.Destiny}" +
                                        $"\nData: {flightCreated.Date}; Saida: {flightCreated.DepartureTime}; Chegada: {flightCreated.ArrivalTime};" + 
                                        $"\nCodigo do Voo: {flightCreated.Id}\n{Environment.NewLine}", outputResult);
                Assert.Equal(null, resultDeleteFlight);
            }
        }

        [Fact]
        public void Update_ShouldModifyEnterprisetInformed_And_ReturnSucessPhrase()
        {

            //Arrange

            var enterprise = _fixture.Enterprise;
            var enterpriseController = _fixture.EnterpriseController;
            var enterpriseControllerView = _fixture.EnterpriseControllerView;
            string idEnterprise;

            //Act

            using (var output = new StringWriter())
            {
                 //Muda o padrão de saida de dados
                Console.SetOut(output);

                // Act
                enterpriseControllerView.SearchAll();

                // Assert
                var outputResult = output.ToString();
                _output.WriteLine($"{outputResult}");

                idEnterprise = outputResult.Substring(16, 36);

                Assert.Equal($"Codigo Empresa: {idEnterprise}; Empresa: {_fixture.Enterprise.FullName};\n{Environment.NewLine}", outputResult);
            }

            using (var input = new StringReader($"a\n{idEnterprise}\nGOL\ngol.airlines@gmail.com\ngol123"))
            using (var output = new StringWriter())
            {

                Console.SetIn(input);
                Console.SetOut(output);

                enterpriseControllerView.Update(enterprise);

                var outputResult = output.ToString();

                var linesOutputResult = outputResult.Split('\n');

                var mensageSucessResult = linesOutputResult[4];

                Assert.Equal($"Empresa {idEnterprise} foi modificada com sucesso!!!\r", mensageSucessResult);
            }
        }
    }
}