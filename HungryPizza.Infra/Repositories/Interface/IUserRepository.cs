using HungryPizza.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPizza.Infra.Repositories.Interface
{
    public interface IUserRepository
    {
        User Add(User entity);
        User GetDetails(int id);
    }
}
