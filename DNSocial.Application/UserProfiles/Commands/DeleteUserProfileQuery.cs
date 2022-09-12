using DNSocial.Application.Models;
using DNSocial.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.UserProfiles.Commands
{
    public record DeleteUserProfileCommand(Guid UserProfileId) : IRequest<OperationResult<UserProfile>>
    {

    }
}
