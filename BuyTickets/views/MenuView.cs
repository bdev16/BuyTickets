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
                // var optionUser = Convert.ToInt32(Console.ReadLine());
                var optionUser = Console.ReadLine();
                switch (optionUser)
                {
                    case "1":
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
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Saindo do sistema...");
                        exitSystem = true;
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        if (optionUser == "")
                        {
                            Console.WriteLine($"Erro: A opção não pode receber um valor vazio...");
                        }
                        else
                        {
                            Console.WriteLine($"Erro: A opção [{optionUser}] não está presente na lista de opções disponiveis...");
                        }
                        Console.ReadLine();
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
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("            BuyFlights           ");
                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine("[1]Ver voos cadastrados.\n[2]Cadastrar voo.\n[3]Editar voo.\n[4]Excluir voo.\n[5]Conta.\n[0]Sair da conta.");
                var optionUser = Console.ReadLine();
                backToMenu = false;
                switch (optionUser)
                {
                    case "1":
                        Console.Clear();
                        _flightControllerView.SearchByEnterprise(enterprise);
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        _flightControllerView.Create(enterprise);
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        _flightControllerView.Update();
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        _flightControllerView.Delete();
                        Console.ReadKey();
                        break;
                    case "5":
                        while (backToMenu != true)
                        {
                            Console.Clear();
                            Console.WriteLine("[1]Dados.\n[2]Editar dados.\n[0]Voltar");
                            var optionUser2 = Console.ReadLine();
                            switch (optionUser2)
                            {
                                case "1":
                                    Console.Clear();
                                    _enterpriseControllerView.SearchById(enterprise);
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    Console.Clear();
                                    _enterpriseControllerView.Update(enterprise);
                                    Console.ReadKey();
                                    break;
                                case "0":
                                    backToMenu = true;
                                    break;
                                default:
                                    Console.Clear();
                                    if (optionUser2 == "")
                                    {
                                        Console.WriteLine($"Erro: A opção não pode receber um valor vazio...");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"A opção [{optionUser2}] não está presente na lista de opções disponiveis...");
                                    }
                                    Console.ReadKey();
                                    break;             
                            }
                        }
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Voltando para o menu principal...");
                        exitAccount = true;
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        if (optionUser == "")
                        {
                            Console.WriteLine($"Erro: A opção não pode receber um valor vazio...");
                        }
                        else
                        {
                            Console.WriteLine($"A opção [{optionUser}] não está presente na lista de opções disponiveis...");
                        }
                        Console.ReadKey();
                        break;
                }
            } 
        }
    }
}