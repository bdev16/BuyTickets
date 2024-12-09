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
            
            EnterpriseController enterpriseController = _fixture.EnterpriseController;
            EnterpriseControllerView enterpriseControllerView = _fixture.EnterpriseControllerView;

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

                // Buscando o voo pelo ID
                var resultCreateEnterprise = enterpriseController.SearchById(Guid.Parse(idEnterprise));

                // //Excluindo o Voo criado
                // EnterpriseController.Delete(Guid.Parse(idEnterprise));

                // //Verificando se o voo foi excluido
                // var resultDeleteFlight = flightController.SearchById(Guid.Parse(idFlight));

                // // Validando que o voo foi criado corretamente
                // Assert.NotNull(resultCreateFlight);
                Assert.Equal(2, 5);
                // Assert.Equal(null, resultDeleteFlight);

            }
        }
    }
}