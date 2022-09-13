using AutoMapper;
using DNSocial.Api.Contracts.Identity;
using DNSocial.Api.Filters;
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
            return Ok();
        }
    }
}
