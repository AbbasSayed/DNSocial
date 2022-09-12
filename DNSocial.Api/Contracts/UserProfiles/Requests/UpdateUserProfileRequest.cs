using System.ComponentModel.DataAnnotations;

namespace DNSocial.Api.Contracts.UserProfiles.Requests
{
    public class UpdateUserProfileRequest
    {

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string Phone { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        public string CurrentCity { get; set; }
    }
}
