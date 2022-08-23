using HungryPizza.Infra.Infrastructure.Interface;
using Microsoft.Extensions.Configuration;

namespace HungryPizza.Infra.Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString => _configuration.GetConnectionString("DefaultConnection");
    }
}
