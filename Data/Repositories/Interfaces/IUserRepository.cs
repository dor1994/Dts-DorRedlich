using Data.DtoModels;
using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<(EnumResponse enumResponse, User userEntity)> LoginAsync(UserModel user);
        public Task<bool> SingUpAsync(UserModel user);
    }
}
