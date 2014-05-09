#region Using directives

using System;

#endregion

namespace TxF
{
    public class FileTransactedException : Exception
    {
        public FileTransactedException(string message) : base(message)
        {
        }

        public FileTransactedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}