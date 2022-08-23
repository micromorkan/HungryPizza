using HungryPizza.Api.Mappings;
using HungryPizza.Api.Models;
using HungryPizza.Api.Services.Interface;
using HungryPizza.Infra.Entities;
using HungryPizza.Infra.UnitOfWork;
using HungryPizza.Models;
using HungryPizza.Models.Enums;

namespace HungryPizza.Api.Services
{
    public class UserService : IUserService
    {
        private readonly DbSession _session;
        public UserService(DbSession session)
        {
            _session = session;
        }

        public Task<UserModel> GetById(int id)
        {
            using (var uow = new UnitOfWork(_session))
            {
                var result = uow.UserRepository.GetDetails(id);

                return Task.FromResult(result.MapToUserModel());
            }
        }

        public Task<UserModel> AddUser(UserModel model)
        {
            using (var uow = new UnitOfWork(_session))
            {
                uow.BeginTransaction();

                var entity = uow.UserRepository.Add(model.MapToUserEntity());

                uow.Commit();

                return Task.FromResult(entity.MapToUserModel());
            }
        }
    }
}
