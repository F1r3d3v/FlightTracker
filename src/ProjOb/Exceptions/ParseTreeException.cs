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
