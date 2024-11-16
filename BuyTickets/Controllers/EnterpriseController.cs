using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.interfaces;
using BuyTickets.models;

namespace BuyTickets.Controllers
{
    public class EnterpriseController : IController<Enterprise>, IAuthentication<Enterprise>
    {
        private List<Enterprise> _enterprises;
        public EnterpriseController(List<Enterprise> enterprises)
        {
            _enterprises = enterprises;
        }
        
        public Enterprise Create(Enterprise enterprise)
        {
            if (enterprise == null)
            {
                return null;
            }

            _enterprises.Add(enterprise);
            return enterprise;
        }

        
        public List<Enterprise> SearchAll()
        {
            if (_enterprises.Count == 0)
            {
                return null;
            }
            return _enterprises;
        }

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

        public IEnumerable<Flight> SearchAllFlights(Enterprise enterprise)
        {
             if (enterprise.Flights.Count == 0)
            {
                return null;
            }
            return enterprise.Flights;
        }

        public Enterprise Update(Enterprise enterpriseUpdate)
        {
            var enterprise = _enterprises.FirstOrDefault(e => e.Id == enterpriseUpdate.Id);
            if (enterprise != null)
            {
                enterprise.FullName = enterpriseUpdate.FullName;
                return enterprise;
            }
            else
            {
                return null;
            }
        }

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