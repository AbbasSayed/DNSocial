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
    public class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileByIdQuery, UserProfile>
    {
        private readonly DataContext _ctx;

        public GetUserProfileByIdHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<UserProfile> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(u => u.UserProfileId == request.id);

            return userProfile;

        }
    }
}
