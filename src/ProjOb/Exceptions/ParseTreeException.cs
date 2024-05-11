using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Exceptions
{
    public class ParseTreeException : Exception
    {
        public ParseTreeException()
        {
        }

        public ParseTreeException(string message) : base(message)
        {
        }

        public ParseTreeException(string message, Exception? inner) : base(message, inner)
        {
        }
    }
}
