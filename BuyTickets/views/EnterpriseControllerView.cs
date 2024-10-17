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

        public void SearchAll()
        {
            var enterpriseListResult = _enterpriseController.SearchAll();

            if (enterpriseListResult == null)
            {
                Console.WriteLine("Nenhuma empresa foi cadastrada até o momento...");
            }
            else
            {
                foreach (var enterprise in enterpriseListResult)
                {
                    Console.WriteLine($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.Name};\n");
                }
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
                //O codigo abaixo, cria uma variavel de verificação que ao receber um valor qualquer do usuario
                //Caso o valor informado pelo usuario for vazio, o atributo em questão de empresa vai receber o valor atual dele sem altera-lo
                //Caso o valor informado por diferente de vazio, o atributo em questão vai receber o valor que foi informado pelo usuario, alterando o valor que estava anteriormente
                Console.WriteLine("Os dados informados vão modificar os dados existente de empresa...");
                Console.ReadKey();
                Console.WriteLine("Informe o nome da empresa: ");
                var name = Console.ReadLine();
                if (name == "")
                {
                    enterprise.Name = enterprise.Name;
                }
                else
                {
                    enterpriseResult.Name = name;
                }
                Console.WriteLine("Informe o email: ");
                var email = Console.ReadLine();
                if (email == "")
                {
                    enterprise.Email = enterprise.Email;
                }
                else
                {
                    enterprise.Email = email;
                }
                Console.WriteLine("Informe a senha");
                var password = Console.ReadLine();
                if (password == "")
                {
                    enterprise.Password = enterprise.Password;
                }
                else
                {
                    enterpriseResult.Password = password;
                }
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

        public void Delete(Enterprise enterprise)
        {
            var enterpriseResult = _enterpriseController.SearchById(enterprise.Id);
            if (enterpriseResult == null)
            {
                Console.WriteLine("Empresa não encontrada...");
            }
            else
            {
                var resultDeleteEnterprise = _enterpriseController.Delete(enterpriseResult.Id);

                if (resultDeleteEnterprise == null)
                {
                    Console.WriteLine($"Ocorreu um erro ao tentar deletar o voo informado...");
                }
                else
                {
                    Console.WriteLine($"Empresa {enterpriseResult.Id} deletada com sucesso!!!");
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