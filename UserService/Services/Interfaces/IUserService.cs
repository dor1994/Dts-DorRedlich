using Data.Enums;
using Data.Models;

namespace UserService.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ApiResponse<UserModel, EnumResponse>> Login(UserModel user);
        public Task<ApiResponse<UserModel, EnumResponse>> SingUp(UserModel user);
    }
}
