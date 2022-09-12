using DNSocial.Application.UserProfiles.Commands;
using DNSocial.Dal;
using DNSocial.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.UserProfiles.CommandHandlers
{
    public class CreateUserProfileHandler : IRequestHandler<CreateUserProfileCommand, UserProfile>
    {
        private readonly DataContext _ctx;

        public CreateUserProfileHandler(DataContext ctx)
        {
            _ctx = ctx; 
        }
        public async Task<UserProfile> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var basicInfo = BasicInfo.CreateBasicInfo(
                request.FirstName,
                request.LastName,
                request.EmailAddress,
                request.Phone,
                request.DateOfBirth,
                request.CurrentCity);

            var userProfile = UserProfile.CreateUserProfile(Guid.NewGuid().ToString(), basicInfo);

            await _ctx.UserProfiles.AddAsync(userProfile);
            await _ctx.SaveChangesAsync();

            return userProfile;
        }
    }
}
