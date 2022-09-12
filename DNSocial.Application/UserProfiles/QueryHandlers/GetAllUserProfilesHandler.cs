using DNSocial.Application.UserProfiles.Queries;
using DNSocial.Dal;
using DNSocial.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.UserProfiles.QueryHandlers
{
    public class GetAllUserProfilesHandler : IRequestHandler<GetAllUserProfilesQuery, IEnumerable<UserProfile>>
    {
        private readonly DataContext _ctx;

        public GetAllUserProfilesHandler(DataContext ctx)
        {
            _ctx = ctx;
                   
        }
        public async Task<IEnumerable<UserProfile>> Handle(GetAllUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var userProfiles = await _ctx.UserProfiles.ToListAsync();

            return userProfiles;
        }
    }
}
