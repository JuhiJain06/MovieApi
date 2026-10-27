
using Microsoft.AspNetCore.Mvc;
using System;

namespace MovieAPI.Exceptions
{
    public class BadRequestException: Exception
    {
        public  BadRequestException(string message):base(message)
        {
            

        }

    }
}
