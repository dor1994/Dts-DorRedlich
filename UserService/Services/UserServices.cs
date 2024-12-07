using Data.Enums;
using Data.Models;
using Data.Repositories.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<UserModel, EnumResponse>> Login(UserModel user)
        {
            var result = await _userRepository.LoginAsync(user);

            return new ApiResponse<UserModel, EnumResponse>
            {
                Status = result.enumResponse == EnumResponse.UserFound,
                Message = result.enumResponse switch
                {
                    EnumResponse.UserFound => "User logged in successfully!",
                    EnumResponse.UserNotFound => "Login failed - user doesn't exist.",
                    EnumResponse.WorngPassword => "Login failed - incorrect password.",
                    _ => "An unknown error occurred during login."
                },
                Data = result.enumResponse == EnumResponse.UserFound ? new UserModel
                {
                    Id = result.userEntity.Id,
                    Username = user.Username,
                    Password = user.Password
                } : null,
                EnumMessage = result.enumResponse
            };
        }

        public async Task<ApiResponse<UserModel, EnumResponse>> SingUp(UserModel user)
        {
            var isUserAdded = await _userRepository.SingUpAsync(user);

            return new ApiResponse<UserModel, EnumResponse>
            {
                Status = isUserAdded,
                Message = isUserAdded
                    ? "User registered successfully!"
                    : "Registration failed - username already exists.",
                Data = isUserAdded ? user : null,
                EnumMessage = isUserAdded
                    ? EnumResponse.UserRegistered
                    : EnumResponse.UserNameExist
            };
        }
    }
}
