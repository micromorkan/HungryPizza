using HungryPizza.Api.Services.Interface;
using HungryPizza.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HungryPizza.Api.Auth
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Token"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "Id").Value;

                if (string.IsNullOrEmpty(userId))
                {
                    context.Items["User"] = userService.GetById(Convert.ToInt32(userId));
                }
                else
                {
                    UserModel model = new UserModel();

                    model.Name = jwtToken.Claims.First(x => x.Type == "Name").Value;
                    model.Cpf = jwtToken.Claims.First(x => x.Type == "Cpf").Value;
                    model.ZipCode = jwtToken.Claims.First(x => x.Type == "ZipCode").Value;
                    model.Street = jwtToken.Claims.First(x => x.Type == "Street").Value;
                    model.Email = jwtToken.Claims.First(x => x.Type == "Email").Value;
                    model.Name = jwtToken.Claims.First(x => x.Type == "Name").Value;

                    context.Items["User"] = userService.AddUser(model);
                }
            }
            catch
            {
                
            }
        }
    }
}