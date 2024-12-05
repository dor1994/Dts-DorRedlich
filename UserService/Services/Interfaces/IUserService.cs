using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ApiResponse<UserModel, EnumResponse>> Login(UserModel user);
        public Task<ApiResponse<UserModel, EnumResponse>> SingUp(UserModel user);
    }
}
