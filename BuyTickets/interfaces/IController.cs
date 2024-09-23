using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.interfaces
{
    public interface IController<T>
    {
        void Create(T value);
        List<T> SearchAll();
        void Update(T value);
        void Delete(Guid value);
        T SearchById(Guid value);
    }
}