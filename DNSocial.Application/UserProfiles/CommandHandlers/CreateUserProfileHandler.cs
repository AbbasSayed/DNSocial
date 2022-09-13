using DNSocial.Application.Models;
using DNSocial.Application.UserProfiles.Commands;
using DNSocial.Dal;
using DNSocial.Domain.Aggregates.UserProfileAggregate;
using DNSocial.Domain.Exceptions.UserProfileExceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.UserProfiles.CommandHandlers
{
    public class CreateUserProfileHandler : IRequestHandler<CreateUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public CreateUserProfileHandler(DataContext ctx)
        {
            _ctx = ctx; 
        }
        public async Task<OperationResult<UserProfile>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            BasicInfo basicInfo;
            OperationResult<UserProfile> result = new OperationResult<UserProfile>();
            try
            {
                basicInfo = BasicInfo.CreateBasicInfo(
                request.FirstName,
                request.LastName,
                request.EmailAddress,
                request.Phone,
                request.DateOfBirth,
                request.CurrentCity);
            }
            catch (UserProfileNotValidException ex)
            {
                result.AddError(Enums.ErrorCode.ValidationError, ex.Message);
                return result; 
            }


            // Case Valid
            // ToDo: Update identityId
            var userProfile = UserProfile.CreateUserProfile(Guid.NewGuid().ToString(), basicInfo);

            await _ctx.UserProfiles.AddAsync(userProfile);

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.AddError(Enums.ErrorCode.ServerError, $"Server Error has been happened while saving the new user Profile\n " +
                    $"Error : {ex.Message}");
                return result;
            }

            result.Payload = userProfile;
            return result;
        }
    }
}
