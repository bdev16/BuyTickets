using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;
using BuyTickets.Validations;

namespace BuyTickets.views
{
    public class EnterpriseControllerView
    {
        private EnterpriseController _enterpriseController;
        private GlobalValidations _globalValidations;

        public EnterpriseControllerView(EnterpriseController enterpriseController, GlobalValidations globalValidations)
        { 
            _enterpriseController = enterpriseController;
            _globalValidations = globalValidations;
        }

        public void Create()
        {
            //Essa variavel verifica se a empresa foi registrada ou não
            //Inicia como FALSE para conseguir entrar no loop
            bool enterpriseRegistred = false;
            //O loop está presente para fixar o usuario na tela de cadastro até ele conseguir realizar o cadastro.
            while (enterpriseRegistred != true)
            {
                Console.Clear();
                Console.WriteLine("Informe o nome da empresa: ");
                var name = Console.ReadLine();
                //A condição verifica se o valor informado pelo usuario é uma string vazia, se for a variavel vai receber o valor null
                //Se não ele vai seguir com o valor que foi informado pelo usuario
                if (name == "")
                    name = null;
                Console.WriteLine("Informe o email: ");
                var email = Console.ReadLine();
                if (email == "")
                    email = null;
                Console.WriteLine("Informe a senha");
                var password = Console.ReadLine();
                if (password == "")
                    password = null;
                

                //Vai receber uma lista de notificações referentes as validações dos dados informados pelo usuario
                var resultValidations = _globalValidations.CreateEnterpriseValidate(name, email, password);

                //Se o atributo da lista sucess for falso o usuario vai receber em sua tela uma lista de erros que ocorreram
                //O que vai fazer ele retornar ao inicio do ciclo de cadastro
                //Se não a empresa vai ser registrada na lista presente no controlador de Enterprise e vai atribuir o valor de true
                //Na variavel enterpriseRegistred fazendo com que a estrutura de repetição acabe
                if (resultValidations.Success == false)
                {
                    Console.WriteLine(JsonSerializer.Serialize(resultValidations));
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Enterprise enterprise = new Enterprise(name, email, password);
                    var result = _enterpriseController.Create(enterprise);
                    Console.WriteLine($"Empresa {result.Id} cadastrada com sucesso!!!");
                    enterpriseRegistred = true;
                    Console.ReadKey();
                }
            }
            
            // if (result == null)
            // {
            //     Console.WriteLine("Ocorreu um erro no cadastro da empresa...");
            // }
            // else
            // {
            //     Console.WriteLine($"Empresa {result.Id} cadastrada com sucesso!!!");
            // }
        }

        public void SearchAll()
        {
            // try
            // {
            //     var enterpriseListResult = _enterpriseController.SearchAll();

            //     foreach (var enterprise in enterpriseListResult)
            //     {
            //         Console.WriteLine($"Codigo Empresa: {enterprise.Id}; Empresa: {enterprise.Name};\n");
            //     }
            // }
            // catch ()
            // {
            //     Console.WriteLine("Nenhuma empresa foi cadastrada até o momento...");
            // }

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
            try
            {
                var enterpriseResult = _enterpriseController.SearchById(enterprise.Id);
                if (enterpriseResult == null)
                {
                    Console.WriteLine("A empresa informada não foi encontrada...");
                }
                else
                {
                    Console.WriteLine("Os dados informados vão modificar os dados do voo existente.");
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
                        Console.Clear();
                        Console.WriteLine("Ocorreu um erro ao tentar modificar os dados da empresa...");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Empresa {enterpriseResult.Id} foi modificada com sucesso!!!");
                    }
                }    
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("O id informado não segue o padrão de estrutura do tipo de ID utilizado dentro do sistema...");
            }
        }

        public void Delete(Enterprise enterprise)
        {
            try
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
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("O id informado não segue o padrão de estrutura do tipo de ID utilizado dentro do sistema...");
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