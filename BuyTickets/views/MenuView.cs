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
   
    }
}