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
    }
}