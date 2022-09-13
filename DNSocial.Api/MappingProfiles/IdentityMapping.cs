using AutoMapper;
using DNSocial.Api.Contracts.Identity.Requests;
using DNSocial.Application.Identity.Commands;

namespace DNSocial.Api.MappingProfiles
{
    public class IdentityMapping : Profile
    {
        public IdentityMapping()
        {
            CreateMap<UserRegistration, RegisterIdentityCommand>();
        }
    }
}
