using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.interfaces
{
    public interface IController
    {
        public void Create();
        public void SearchAll();
        public void Update();
        public void Delete();
        public void SearchById();
    }
}