using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.interfaces;
using BuyTickets.models;

namespace BuyTickets.Controllers
{
    /// <summary>
    /// This class represents the business logic of the Enterprise class, containing the methods to Create, Update, List and remove a class from a Enterprise List
    /// </summary>
    public class EnterpriseController : IController<Enterprise>
    {
        private List<Enterprise> _enterprises = new List<Enterprise>();

        /// <summary>
        /// Will add enterprise in the enterprise list.
        /// </summary>
        /// <param name="enterprise">Will receive a Enterprise class Object</param>
        public void Create(Enterprise enterprise)
        {
             if (enterprise == null)
            {
                Console.WriteLine("Ocorreu ao tentar cadastrar a Empresa...");
            }

            _enterprises.Add(enterprise);
            Console.WriteLine($"Empresa {enterprise.Id} criada com sucesso!!!");
        }

        /// <summary>
        /// This method provides access to a copy of the Enterprise list.
        /// </summary>
        /// <returns>Will return the enterprise list.</returns>
        public List<Enterprise> SearchAll()
        {
            return _enterprises;
        }

        /// <summary>
        /// You will search for a enterprise in the enterprise list using your Id attribute.
        /// </summary>
        /// <param name="idEnterprise">Will receive a Guid class which will represent the Enterprise ID attribute.</param>
        /// <returns>Will returning the found class</returns>
        public Enterprise SearchById(Guid idEnterprise)
        {
            var resultSearchById = _enterprises.FirstOrDefault(e => e.Id == idEnterprise);
            if (resultSearchById != null)
            {
                return resultSearchById;
            }
            else
            {
                return resultSearchById;
            }
        }

        /// <summary>
        /// It will modify the attributes of a Enterprise already present in the Enterprise List.
        /// </summary>
        /// <param name="enterpriseUpdate">Will receive a Enterprise class Object.</param>
        public void Update(Enterprise enterpriseUpdate)
        {
            var enterprise = _enterprises.FirstOrDefault(e => e.Id == enterpriseUpdate.Id);
            if (enterprise != null)
            {
                enterprise.Name = enterpriseUpdate.Name;
            }
            else
            {
                Console.WriteLine("O Codigo informado não corresponde a nenhuma das Empresas cadastradas no sistema...");
            }
        }

        /// <summary>
        /// This method will remove the Enterprise class for the enterprise list. 
        /// </summary>
        /// <param name="idEnterprise">Will receive a Guid class which will represent the Enterprise ID attribute.</param>
        public void Delete(Guid idEnterprise)
        {
            var resultSearchById = _enterprises.FirstOrDefault(e => e.Id == idEnterprise);
            if (resultSearchById != null)
            {
                _enterprises.Remove(resultSearchById);
            }
            else
            {
                Console.WriteLine("O Codigo informado não corresponde a nenhuma das empresas cadastradas no sistema...");
            }
        }
    }
}