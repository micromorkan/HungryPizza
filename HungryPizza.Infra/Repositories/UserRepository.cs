using Dapper;
using HungryPizza.Infra.Entities;
using HungryPizza.Infra.Repositories.Interface;
using HungryPizza.Infra.UnitOfWork;

namespace HungryPizza.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbSession _session;

        public UserRepository(DbSession session)
        {
            _session = session;
        }

        public User Add(User entity)
        {
            entity.Id = _session.Connection.ExecuteScalar<int>(
                "INSERT INTO [USER] (Name, Cpf, Email, ZipCode, City, Street, Complement, Reference) VALUES (@Name, @Cpf, @Email, @ZipCode, @City, @Street, @Complement, @Reference);" +
                "SELECT SCOPE_IDENTITY()",
                entity,
                _session.Transaction
                );

            return entity;
        }

        public User GetDetails(int id)
        {
            return _session.Connection.Query<User>("SELECT Id, Name, Cpf, Email, ZipCode, City, Street, Complement, Reference FROM [USER] WHERE Id = @id",
                new { id = id },
                _session.Transaction).FirstOrDefault();
        }
    }
}
