using DNSocial.Application.Identity.Commands;
using DNSocial.Application.Models;
using DNSocial.Application.Options;
using DNSocial.Dal;
using DNSocial.Domain.Aggregates.UserProfileAggregate;
using DNSocial.Domain.Exceptions.UserProfileExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.Identity.Handlers
{
    public class RegisterIdentityHandler : IRequestHandler<RegisterIdentityCommand, OperationResult<string>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public RegisterIdentityHandler(DataContext ctx, UserManager<IdentityUser> userManager,  IOptions<JwtSettings> jwtSettings)
        {
            _ctx = ctx;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }


        // ToDo: We need to refactor this method!
        public async Task<OperationResult<string>> Handle(RegisterIdentityCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<string>();

            try
            {
                var existingIdentity = await _userManager.FindByEmailAsync(request.UserName);
                if (existingIdentity != null)
                {
                    result.AddError(Enums.ErrorCode.IdentityUserAleadyExists, 
                        "Provided email address already exists. Cannot register register new user");
                    return result;
                }

                var identity = new IdentityUser()
                {
                    UserName = request.UserName,
                    Email = request.UserName,
                };

                // We need all of these operation happen togather, so we will create a transaction
                using var transaction = _ctx.Database.BeginTransaction();

                var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
                if (!createdIdentity.Succeeded)
                {

                    await transaction.RollbackAsync();

                    foreach (var err in createdIdentity.Errors)
                    {
                        result.AddError(Enums.ErrorCode.IdentityCreationFailed, err.Description);
                    }
                    return result;
                }

                var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName,
                    request.LastName,
                    request.UserName,
                    request.Phone,
                    request.DateOfBirth,
                    request.CurrentCity);

                var userProfile = UserProfile.CreateUserProfile(identity.Id, profileInfo);

                _ctx.UserProfiles.Add(userProfile);


                // when any exception happen we will need to rollback
                try
                {
                    await _ctx.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
                

                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
                var TokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                        new Claim("IdentityId", identity.Id),
                        new Claim("UserProfileId", userProfile.UserProfileId.ToString())
                    }),
                    Expires = DateTime.Now.AddHours(2),
                    Audience = _jwtSettings.Audiences[0],
                    Issuer = _jwtSettings.Issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(TokenDescriptor);

                result.Payload = tokenHandler.WriteToken(token);
            }
            catch (UserProfileNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e => result.AddError(Enums.ErrorCode.ValidationError, e));
            }
            catch (Exception ex)
            {
                result.AddDefaultError(ex.Message);    
            }

            return result;
        }
    }
}
