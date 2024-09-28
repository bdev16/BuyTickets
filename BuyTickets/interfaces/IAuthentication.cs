using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyTickets.interfaces
{
    public interface IAuthentication<T>
    {
        T Login(string value, string value2);
    }
}