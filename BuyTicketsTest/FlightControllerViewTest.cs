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

    }
}