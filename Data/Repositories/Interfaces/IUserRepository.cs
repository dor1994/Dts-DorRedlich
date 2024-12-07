using Data.DtoModels;
using Data.Enums;
using Data.Models;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<(EnumResponse enumResponse, User userEntity)> LoginAsync(UserModel user);
        public Task<bool> SingUpAsync(UserModel user);
    }
}
