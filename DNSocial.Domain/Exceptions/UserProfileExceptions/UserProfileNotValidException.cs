using DNSocial.Domain.Exceptions.CommonExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Domain.Exceptions.UserProfileExceptions
{
    public  class UserProfileNotValidException : NotValidException
    {
        internal UserProfileNotValidException() { }
        internal UserProfileNotValidException(string message) : base(message) { }
        internal UserProfileNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
