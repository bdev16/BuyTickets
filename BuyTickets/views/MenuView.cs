using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.models;


namespace BuyTickets.views
{
    public class MenuView
    {
        private readonly FlightControllerView _flightControllerView;
        private readonly EnterpriseControllerView _enterpriseControllerView;

        public MenuView(FlightControllerView flightControllerView, EnterpriseControllerView enterpriseControllerView)
        {
            _flightControllerView = flightControllerView;
            _enterpriseControllerView = enterpriseControllerView;
        }

        public void MainMenu(MenuView menuView)
        {
            bool exitSystem = false;

            while (exitSystem != true)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("            BuyFlights           ");
                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine("[1]Empresas.\n[0]Sair do sistema.");
                var optionUser = Convert.ToInt32(Console.ReadLine());
                switch (optionUser)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("[1]Login.\n[2]Cadastro.");
                        var optionUser2 = Convert.ToInt32(Console.ReadLine());
                        switch(optionUser2)
                        {
                            case 1:
                                _enterpriseControllerView.Login(menuView);
                                break;
                            case 2:
                                _enterpriseControllerView.Create();
                                break;
                        }
                        Console.ReadKey();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Saindo do sistema...");
                        exitSystem = true;
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine($"A opção [{optionUser}] não está presente na lista de opções disponiveis...");
                        break;
                }                 
            }
        }
   
        public void EnterpriseMenu(Enterprise enterprise)
        {
            bool exitAccount = false;
            bool backToMenu = false;

            while (exitAccount != true)
            {
                //A estrutura Try Catch vai evitar que ocorra um exceção, relacionada a informação do valor que é relacionado as opções presentes
                //Sem o try catch quando o usuario não informa nenhum valor, o programa para e retorna uma exception. Com o Try Catch isso é evitado
                try
                {
                    Console.Clear();
                    Console.WriteLine("=================================");
                    Console.WriteLine("            BuyFlights           ");
                    Console.WriteLine("=================================");
                    Console.WriteLine();
                    Console.WriteLine("[1]Ver voos cadastrados.\n[2]Cadastrar voo.\n[3]Editar voo.\n[4]Excluir voo.\n[5]Conta.\n[0]Sair da conta.");
                    var optionUser = Convert.ToChar(Console.ReadLine());
                    switch (optionUser)
                    {
                        case '1':
                            Console.Clear();
                            _flightControllerView.SearchByEnterprise(enterprise);
                            Console.ReadKey();
                            break;
                        case '2':
                            Console.Clear();
                            _flightControllerView.Create(enterprise);
                            Console.ReadKey();
                            break;
                        case '3':
                            Console.Clear();
                            _flightControllerView.Update();
                            Console.ReadKey();
                            break;
                        case '4':
                            Console.Clear();
                            _flightControllerView.Delete();
                            Console.ReadKey();
                            break;
                        case '5':
                            while (backToMenu != true)
                            {
                                Console.Clear();
                                Console.WriteLine("[1]Dados.\n[2]Editar dados.\n[0]Voltar");
                                var optionUser2 = Convert.ToInt32(Console.ReadLine());
                                switch (optionUser2)
                                {
                                    case 1:
                                        Console.Clear();
                                        _enterpriseControllerView.SearchById(enterprise);
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        Console.Clear();
                                        _enterpriseControllerView.Update(enterprise);
                                        Console.ReadKey();
                                        break;
                                    case 0:
                                        backToMenu = true;
                                        break;
                                }
                            }
                            break;
                        case '0':
                            Console.Clear();
                            Console.WriteLine("Voltando para o menu principal...");
                            exitAccount = true;
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine($"A opção [{optionUser}] não está presente na lista de opções disponiveis...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("O valor informado não é valido. Informe outro valor");
                    Console.ReadKey();
                }
            }
        }
    }
}