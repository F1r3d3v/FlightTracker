namespace ProjOb.Exceptions
{
    public class DataIntegrityException : Exception
    {
        public DataIntegrityException()
        {
        }

        public DataIntegrityException(string message) : base(message)
        {
        }

        public DataIntegrityException(string message, Exception? inner) : base(message, inner)
        {
        }
    }
}