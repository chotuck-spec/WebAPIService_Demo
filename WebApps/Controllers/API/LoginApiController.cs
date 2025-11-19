using Microsoft.AspNetCore.Mvc;

namespace WebApps.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginApiController : Controller
    {
        [HttpGet]
        public IActionResult GetLogin([FromQuery] string userId, [FromQuery] string password)
        {
            // Simple demo validation
            if (userId == "admin" && password == "123")
            {
                return Ok(new { message = "Login successful" });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
}
