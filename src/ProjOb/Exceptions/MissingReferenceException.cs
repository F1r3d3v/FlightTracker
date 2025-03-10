﻿namespace ProjOb.Exceptions
{
    public class MissingReferenceException : Exception
    {
        public MissingReferenceException()
        {
        }

        public MissingReferenceException(string message) : base(message)
        {
        }

        public MissingReferenceException(string message, Exception? inner) : base(message, inner)
        {
        }
    }
}