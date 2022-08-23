using HungryPizza.Infra.Entities;
using HungryPizza.Models;

namespace HungryPizza.Api.Mappings
{
    public static class UserMapper
    {
        public static UserModel MapToUserModel(this User entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Cpf = entity.Cpf,
                Email = entity.Email,
                City = entity.City,
                Complement = entity.Complement,
                Reference = entity.Reference,
                Street = entity.Street,
                ZipCode = entity.ZipCode
            };
        }

        public static User MapToUserEntity(this UserModel model)
        {
            return new User
            {
                Id = model.Id,
                Name = model.Name,
                Cpf = model.Cpf,
                Email = model.Email,
                City = model.City,
                Complement = model.Complement,
                Reference = model.Reference,
                Street = model.Street,
                ZipCode = model.ZipCode
            };
        }
    }
}
