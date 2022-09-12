using DNSocial.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.UserProfiles.Queries
{
    public class GetAllUserProfilesQuery : IRequest<IEnumerable<UserProfile>>
    {
    }
}
