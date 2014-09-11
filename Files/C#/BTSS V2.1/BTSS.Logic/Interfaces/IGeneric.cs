using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces
{
    public interface IGeneric<T>
    {
        void Save(T entity); 

    }
}
