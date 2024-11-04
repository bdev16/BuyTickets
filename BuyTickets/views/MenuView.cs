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
        private readonly CustomerControllerView _customerControllerView;

        public MenuView(FlightControllerView flightControllerView, EnterpriseControllerView enterpriseControllerView, CustomerControllerView customerControllerView)
        {
            _flightControllerView = flightControllerView;
            _enterpriseControllerView = enterpriseControllerView;
            _customerControllerView = customerControllerView;
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
                Console.WriteLine("[1]Empresas.\n[2]Clientes.\n[0]Sair do sistema.");
                // var optionUser = Convert.ToInt32(Console.ReadLine());
                var optionUser = Console.ReadLine();
                switch (optionUser)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("[1]Login.\n[2]Cadastro.");
                        var optionUser2 = Console.ReadLine();
                        switch(optionUser2)
                        {
                            case "1":
                                Console.Clear();
                                _enterpriseControllerView.Login(menuView);
                                break;
                            case "2":
                                Console.Clear();
                                _enterpriseControllerView.Create();
                                break;
                        }
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("[1]Login.\n[2]Cadastro.");
                        var optionUser3 = Console.ReadLine();
                        switch(optionUser3)
                        {
                            case "1":
                                Console.Clear();
                                _customerControllerView.Login(menuView);
                                break;
                            case "2":
                                Console.Clear();
                                _customerControllerView.Create();
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
                Console.WriteLine("[1]Ver voos cadastrados.\n[2]Pesquisar voo.\n[3]Cadastrar voo.\n[4]Editar voo.\n[5]Excluir voo.\n[6]Conta.\n[0]Sair da conta.");
                var optionUser = Console.ReadLine();
                backToMenu = false;
                switch (optionUser)
                {
                    case "1":
                        Console.Clear();
                        _enterpriseControllerView.SearchAllFlights(enterprise);
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        _flightControllerView.SearchByIdWithCustomers();
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        _flightControllerView.Create(enterprise);
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        _flightControllerView.Update();
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Clear();
                        _flightControllerView.Delete();
                        Console.ReadKey();
                        break;
                    case "6":
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

        public void CustomerMenu(Customer customer)
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
                Console.WriteLine("[1]Ver voos.\n[2]Minhas Viagens.\n[3]Pesquisar por Voos.\n[4]Comprar passagem.\n[5]Conta.\n[0]Sair da conta.");
                var optionUser = Console.ReadLine();
                backToMenu = false;
                switch (optionUser)
                {
                    case "1":
                        Console.Clear();
                        _flightControllerView.SearchAll();
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        _customerControllerView.SearchAllFlights(customer);
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        _flightControllerView.FlightFilter();
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        _customerControllerView.BuyFlight(customer);
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
                                    _customerControllerView.SearchById(customer);
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    Console.Clear();
                                    _customerControllerView.Update(customer);
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