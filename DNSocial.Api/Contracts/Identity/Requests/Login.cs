using System.ComponentModel.DataAnnotations;

namespace DNSocial.Api.Contracts.Identity.Requests
{
    public class Login
    {
        [EmailAddress]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
