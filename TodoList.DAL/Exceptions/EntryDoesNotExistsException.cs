using System;
using System.Runtime.Serialization;

namespace TodoList.DAL.Repositories.Exceptions
{
    [Serializable]
    public class EntryDoesNotExistsException : Exception
    {
        public EntryDoesNotExistsException()
        {
        }

        public EntryDoesNotExistsException(string message) : base(message)
        {
        }

        public EntryDoesNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntryDoesNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}