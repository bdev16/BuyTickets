using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.interfaces
{
    public interface IController<T>
    {
        T Create(T value);
        List<T> SearchAll();
        T Update(T value);
        bool Delete(Guid value);
        T SearchById(Guid value);
    }
}