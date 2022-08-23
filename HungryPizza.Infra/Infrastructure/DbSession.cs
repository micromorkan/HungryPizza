using System.Data;
using System.Data.SqlClient;

namespace HungryPizza.Infra.Infrastructure
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            Connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=HungryPizza;Trusted_Connection=True;MultipleActiveResultSets=true");
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
