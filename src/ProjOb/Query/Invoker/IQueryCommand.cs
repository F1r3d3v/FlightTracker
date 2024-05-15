using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Invoker
{
    public interface IQueryCommand
    {
        void Execute();
    }

    public interface IQueryCommand<T> : IQueryCommand
    {
        T getResult();
    }
}
