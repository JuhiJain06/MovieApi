using System;

namespace MovieAPI.Exceptions
{
    public class UnauthorizedException:Exception
    {
        public UnauthorizedException(string message):base(message)
        {
            
        }
    }
}
