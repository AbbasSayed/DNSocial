using AutoMapper;
using DNSocial.Api.Contracts.Identity.Requests;
using DNSocial.Api.Contracts.Identity.Responses;
using DNSocial.Api.Filters;
using DNSocial.Application.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DNSocial.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRouts.BaseRoute)]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public IdentityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route(ApiRouts.Identity.Registration)]
        [ValidateModel]
        public async Task<IActionResult> Register(UserRegistration userRegistration)
        {
            var command = _mapper.Map<RegisterIdentityCommand>(userRegistration);
            var result = await _mediator.Send(command);

            if (result.IsError)
            {
                return this.HandleErrorResponse(result.Errors);
            }

            var authResult = new AuthenticationResult() { Token = result.Payload };


            return Ok(authResult);
        }
    }
}
