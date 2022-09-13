using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.Enums
{
    public enum ErrorCode
    {
        NotFound = 404,
        ServerError = 500,
        UnknownError = 999,

        // Validation errors should be in range 100 - 199
        ValidationError = 101,

        // Infrastracture errors should be in range 200 - 299
        IdentityUserAleadyExists = 201,
        IdentityCreationFailed = 202,

    }
}
