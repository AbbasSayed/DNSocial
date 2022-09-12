using DNSocial.Application.Models;
using DNSocial.Application.UserProfiles.Commands;
using DNSocial.Dal;
using DNSocial.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public UpdateUserProfileHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

            var result = new OperationResult<UserProfile>();

            if (userProfile is null)
            {
                result.AddError(Enums.ErrorCode.NotFound, $"There is no user profile for the id : {request.UserProfileId}");

                return result;
            }

            try
            {
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName,
                    request.LastName, request.EmailAddress, request.Phone,
                    request.DateOfBirth, request.CurrentCity);

                userProfile.UpdateBasicInfo(basicInfo);

                _ctx.UserProfiles.Update(userProfile);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = userProfile;
                return result;


            }
            catch (Exception e)
            {
                result.AddDefaultError("Unhandled Domain Validation");
                return result;
            }
            

        }
    }
}
