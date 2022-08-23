using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HungryPizza.Api.Auth
{
    public class JwtManager
    {
        private readonly IConfiguration _configuration;

        public JwtManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Authenticate()
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Token"]);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                //COMO EXEMPLO DE TOKEN, DEVE-SE INFORMAR O ID OU OS OUTROS ELEMENTOS NECESSÁRIOS PARA REALIZAR O PEDIDO
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", ""),
                    new Claim(ClaimTypes.Name, ""),
                    new Claim(ClaimTypes.Email, ""),
                    new Claim("Cpf", ""),
                    new Claim("ZipCode", ""),
                    new Claim("City", ""),
                    new Claim("Street", ""),
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature) 
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
