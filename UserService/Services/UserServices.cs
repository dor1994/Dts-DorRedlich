using Data.Enums;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ApiResponse<UserModel, EnumResponse> response = new ApiResponse<UserModel, EnumResponse>();
            var result = await _userRepository.LoginAsync(user);

            switch (result.enumResponse)
            {
                case EnumResponse.UserFound:
                    user.Id = result.userEntity.Id;
                    response.Status = true;
                    response.Message = "User Login successfully!";
                    response.Data = user;
                    response.EnumMessage = EnumResponse.UserFound;
                    break;
                case EnumResponse.UserNotFound:
                    response.Status = false;
                    response.Message = "Login user failed - user don't exist";
                    response.EnumMessage = EnumResponse.UserNotFound;
                    break;
                case EnumResponse.WorngPassword:
                    response.Status = false;
                    response.Message = "Login user failed - Worng Password";
                    response.EnumMessage = EnumResponse.WorngPassword;
                    break;
                default:
                    break;
            }
         
            return response;
        }

        public async Task<ApiResponse<UserModel, EnumResponse>> SingUp(UserModel user)
        {
            ApiResponse<UserModel, EnumResponse> response = new ApiResponse<UserModel, EnumResponse>();
            var isUserAdd = await _userRepository.SingUpAsync(user);

            if (isUserAdd)
            {
                response.Status = true;
                response.Message = "User registered successfully!";
                response.Data = user;
            }

            response.Status = false;
            response.Message = "Registered user failed - the userName is already exist";
            response.EnumMessage = EnumResponse.UserNameExist;

            return response;
        }
    }
}
