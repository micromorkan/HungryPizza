namespace HungryPizza.Infra.Infrastructure.Interface
{
    public interface IConnectionFactory
    {
        public string GetConnectionString { get; }
    }
}
