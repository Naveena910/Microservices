using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    
        public  class Exceptions : Exception
        {
            public Exceptions(string message) : base(message)
            {

            }
        }

        public class ConflictException : Exceptions
        {
            public ConflictException(string message) : base(message)
            {

            }
        }
    public class ForbiddenException : Exceptions
    {
        public ForbiddenException(string message) : base(message)
        {

        }
    }

    public class UnauthorizedException : Exceptions
        {
            public UnauthorizedException(string message) : base(message)
            {

            }
        }

       
        public class NotFoundException : Exceptions
        {
            public NotFoundException(string message) : base(message)
            {

            }
        }
    public class BadRequestException : Exceptions
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
    public class InternalServerException : Exceptions
    {
        public InternalServerException(string message) : base(message)
        {

        }
    }
}

