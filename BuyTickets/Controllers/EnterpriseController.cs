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
    public class EnterpriseController : IController<Enterprise>, IAuthentication<Enterprise>
    {
        private List<Enterprise> _enterprises;
        public EnterpriseController(List<Enterprise> enterprises)
        {
            _enterprises = enterprises;
        }
        /// <summary>
        /// Will add enterprise in the enterprise list.
        /// </summary>
        /// <param name="enterprise">Will receive a Enterprise class Object</param>
        public Enterprise Create(Enterprise enterprise)
        {
            if (enterprise == null)
            {
                return null;
            }

            _enterprises.Add(enterprise);
            return enterprise;
        }

        /// <summary>
        /// This method provides access to a copy of the Enterprise list.
        /// </summary>
        /// <returns>Will return the enterprise list.</returns>
        public List<Enterprise> SearchAll()
        {
            if (_enterprises.Count == 0)
            {
                return null;
            }
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
        public Enterprise Update(Enterprise enterpriseUpdate)
        {
            var enterprise = _enterprises.FirstOrDefault(e => e.Id == enterpriseUpdate.Id);
            if (enterprise != null)
            {
                enterprise.Name = enterpriseUpdate.Name;
                return enterprise;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will remove the Enterprise class for the enterprise list. 
        /// </summary>
        /// <param name="idEnterprise">Will receive a Guid class which will represent the Enterprise ID attribute.</param>
        public bool Delete(Guid idEnterprise)
        {
            var resultSearchById = _enterprises.FirstOrDefault(e => e.Id == idEnterprise);
            if (resultSearchById != null)
            {
                _enterprises.Remove(resultSearchById);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Enterprise Login(string emailEnterprise, string passwordEnterprise)
        {
            var resultSearchByEmailAndPassword = _enterprises.FirstOrDefault(e => e.Email == emailEnterprise && e.Password == passwordEnterprise);
            if (resultSearchByEmailAndPassword == null)
            {
                return null;
            }

            return resultSearchByEmailAndPassword;
        }
    }
}