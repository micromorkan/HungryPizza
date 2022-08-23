using HungryPizza.Api.Auth;
using HungryPizza.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HungryPizza.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class LoginController : ControllerBase
    {
        private readonly JwtManager jwtAuthenticationManager;
        public LoginController(JwtManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost]
        public IActionResult GenerateToken()
        {
            var token = jwtAuthenticationManager.Authenticate();
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }
    }
}
