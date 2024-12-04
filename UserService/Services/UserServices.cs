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
        public async Task<ApiResponse<UserModel, EnumError>> Login(UserModel user)
        {
            ApiResponse<UserModel, EnumError> response = new ApiResponse<UserModel, EnumError>();
            var isUserAdd = await _userRepository.LoginAsync(user);

            switch (isUserAdd)
            {
                case EnumError.UserFound:
                    response.Status = true;
                    response.Message = "User Login successfully!";
                    response.Data = user;
                    response.EnumMessage = EnumError.UserFound;
                    break;
                case EnumError.UserNotFound:
                    response.Status = false;
                    response.Message = "Login user failed - user don't exist";
                    response.EnumMessage = EnumError.UserNotFound;
                    break;
                case EnumError.WorngPassword:
                    response.Status = false;
                    response.Message = "Login user failed - Worng Password";
                    response.EnumMessage = EnumError.WorngPassword;
                    break;
                default:
                    break;
            }
         
            return response;
        }

        public async Task<ApiResponse<UserModel, EnumError>> SingUp(UserModel user)
        {
            ApiResponse<UserModel, EnumError> response = new ApiResponse<UserModel, EnumError>();
            var isUserAdd = await _userRepository.SingUpAsync(user);

            if (isUserAdd)
            {
                response.Status = true;
                response.Message = "User registered successfully!";
                response.Data = user;
            }

            response.Status = false;
            response.Message = "Registered user failed - the userName is already exist";
            response.EnumMessage = EnumError.UserNameExist;

            return response;
        }
    }
}
