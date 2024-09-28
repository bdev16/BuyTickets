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
        public void Create(Enterprise value)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid value)
        {
            throw new NotImplementedException();
        }

        public List<Enterprise> SearchAll()
        {
            throw new NotImplementedException();
        }

        public Enterprise SearchById(Guid value)
        {
            throw new NotImplementedException();
        }

        public void Update(Enterprise value)
        {
            throw new NotImplementedException();
        }
    }
}