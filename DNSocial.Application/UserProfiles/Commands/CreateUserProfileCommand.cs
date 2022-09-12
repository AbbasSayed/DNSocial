﻿using DNSocial.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Application.UserProfiles.Commands
{
    public class CreateUserProfileCommand : IRequest<UserProfile>
    {
        public string FirstName { get;  set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get;  set; }
        public string CurrentCity { get; set; }
    }
}