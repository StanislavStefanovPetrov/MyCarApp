using System;

namespace MyCarApp.Services.Exceptions
{
    public class MyCarAppException : Exception
    {
        private new const string Message = "An exception appeared.";

        public MyCarAppException() : base(Message)
        {
        }

        public MyCarAppException(string message) : base(message)
        {
        }
    }
}
