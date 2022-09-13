using AutoMapper;
using DNSocial.Api.Contracts.UserProfiles.Requests;
using DNSocial.Api.Contracts.UserProfiles.Responses;
using DNSocial.Api.Filters;
using DNSocial.Application.Models;
using DNSocial.Application.UserProfiles.Commands;
using DNSocial.Application.UserProfiles.Queries;
using DNSocial.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DNSocial.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserProfilesController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserProfiles()
        {
            var userProfiles = await _mediator.Send<IEnumerable<UserProfile>>(new GetAllUserProfilesQuery());
            var userProfileResponse =  _mapper.Map<IEnumerable<UserProfileResponse>>(userProfiles);
            return Ok(userProfileResponse);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var userProfile = await _mediator.Send<UserProfile>(new GetUserProfileByIdQuery(Guid.Parse(id)));
            var userProfileResponse = _mapper.Map<UserProfileResponse>(userProfile);
            
            return Ok(userProfileResponse);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateUserProfile([FromBody] CreateUserProfileRequest request)
        {
            var createUserProfileCommand = _mapper.Map<CreateUserProfileCommand>(request);
            var result = await _mediator.Send<OperationResult<UserProfile>>(createUserProfileCommand);
            if (result.IsError)
            {
                return this.HandleErrorResponse(result.Errors);
            }
            var userProfileResponse = _mapper.Map<UserProfileResponse>(result.Payload);
            return CreatedAtAction(nameof(GetById), new { id = userProfileResponse.UserProfileId }, userProfileResponse);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserProfile(string id, [FromBody] UpdateUserProfileRequest request)
        {
            var updateUserProfilecommand = _mapper.Map<UpdateUserProfileCommand>(request);
            updateUserProfilecommand.UserProfileId = Guid.Parse(id);
            var result = await _mediator.Send<OperationResult<UserProfile>>(updateUserProfilecommand);

            return result.IsError ? this.HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var command = new DeleteUserProfileCommand(Guid.Parse(id));
            var response = await _mediator.Send<OperationResult<UserProfile>>(command);

            return response.IsError ? this.HandleErrorResponse(response.Errors) : NoContent();
        }
    }
}
