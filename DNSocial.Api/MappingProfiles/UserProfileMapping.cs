using AutoMapper;
using DNSocial.Api.Contracts.UserProfiles.Requests;
using DNSocial.Api.Contracts.UserProfiles.Responses;
using DNSocial.Application.UserProfiles.Commands;
using DNSocial.Domain.Aggregates.UserProfileAggregate;

namespace DNSocial.Api.MappingProfiles
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();
            CreateMap<CreateUserProfileRequest, CreateUserProfileCommand>();
            CreateMap<UpdateUserProfileRequest, UpdateUserProfileCommand>();
        }
    }
}
