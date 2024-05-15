using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Exceptions
{
    public class QueryExecutionException : Exception
    {
        public QueryExecutionException()
        {
        }

        public QueryExecutionException(string message) : base(message)
        {
        }

        public QueryExecutionException(string message, Exception? inner) : base(message, inner)
        {
        }
    }
}
