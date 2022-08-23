using HungryPizza.Api.Models;
using HungryPizza.Models;

namespace HungryPizza.Api.Services.Interface
{
    public interface IUserService
    {
        Task<UserModel> GetById(int id);
        Task<UserModel> AddUser(UserModel model);
    }
}
