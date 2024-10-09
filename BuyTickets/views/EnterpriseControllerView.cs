using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;

namespace BuyTickets.views
{
    public class EnterpriseControllerView
    {
        private EnterpriseController _enterpriseController;

        public EnterpriseControllerView(EnterpriseController enterpriseController)
        { 
            _enterpriseController = enterpriseController;
        }

        public void Create()
        {
            Console.WriteLine("Informe o nome da empresa: ");
            var name = Console.ReadLine();
            Console.WriteLine("Informe o email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Informe a senha");
            var password = Console.ReadLine();
            Enterprise enterprise = new Enterprise(name, email, password);

            var result = _enterpriseController.Create(enterprise);

            if (result == null)
            {
                Console.WriteLine("Ocorreu um erro no cadastro da empresa...");
            }
            else
            {
                Console.WriteLine($"Empresa {result.Id} cadastrada com sucesso!!!");
            }
        }

        public void SearchById(Enterprise enterprise)
        {
            var enterpriseResult = _enterpriseController.SearchById(enterprise.Id);

            if (enterpriseResult == null)
            {
                Console.WriteLine("A empresa informada não foi encontrada...");
            }
            else
            {
                Console.WriteLine($"Nome: {enterpriseResult.Name};\nEmail: {enterpriseResult.Email};\nSenha: {enterpriseResult.Password}");
            }
        }

        public void Update(Enterprise enterprise)
        {
            var enterpriseResult = _enterpriseController.SearchById(enterprise.Id);
            if (enterpriseResult == null)
            {
                Console.WriteLine("A empresa informada não foi encontrada...");
            }
            else
            {
                enterpriseResult.Name = Console.ReadLine();
                enterpriseResult.Email = Console.ReadLine();
                enterpriseResult.Password = Console.ReadLine();
                var enterpriseUpdateResult = _enterpriseController.Update(enterpriseResult);

                if (enterpriseUpdateResult == null)
                {
                    Console.WriteLine("Ocorreu um erro ao tentar modificar os dados da empresa...");
                }
                else
                {
                    Console.WriteLine($"Empresa {enterpriseResult.Id} foi modificada com sucesso!!!");
                }
            }     
        }

        public void Login(MenuView menuView)
        {
            Console.WriteLine("Informe o email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Informe a senha: ");
            var password = Console.ReadLine();
            var loginResult = _enterpriseController.Login(email, password);

            if (loginResult == null)
            {
                Console.WriteLine("Email ou senha invalidos...");
            }
            else
            {
                menuView.EnterpriseMenu(loginResult);
            }
        }
    }
}