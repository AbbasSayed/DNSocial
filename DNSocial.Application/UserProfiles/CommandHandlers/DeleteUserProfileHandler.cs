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
    public class DeleteUserProfileHandler : IRequestHandler<DeleteUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public DeleteUserProfileHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

            var result = new OperationResult<UserProfile>();

            if (userProfile is null)
            {
                result.AddError(Enums.ErrorCode.NotFound, $"There is no user profile with Id : {request.UserProfileId}");
                return result;
            }

            _ctx.UserProfiles.Remove(userProfile);
            await _ctx.SaveChangesAsync();
            result.Payload = userProfile;
            return result;
        }
    }
}
