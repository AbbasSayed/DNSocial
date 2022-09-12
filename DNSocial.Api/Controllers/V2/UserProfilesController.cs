using Microsoft.AspNetCore.Mvc;

namespace DNSocial.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var user = new { Id = id, Name = "Sayed" };

            return Ok(user);
        }
    }
}
