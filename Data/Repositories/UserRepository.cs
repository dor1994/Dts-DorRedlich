using Azure.Core;
using Data.DtoModels;
using Data.Enums;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(EnumResponse enumResponse, User userEntity)> LoginAsync(UserModel user)
        {

            var userEntity = await _unitOfWork.FirstOrDefaultAsync<User>(x => x.Username == user.Username);

            if(userEntity == null)
            {
               return (EnumResponse.UserNotFound, userEntity);
            }

            else if (userEntity != null && !BCrypt.Net.BCrypt.Verify(user.Password, userEntity.PasswordHash))
                return (EnumResponse.WorngPassword, userEntity);

            return (EnumResponse.UserFound, userEntity);
        }

        public async Task<bool> SingUpAsync(UserModel user)
        {
            var isExist = await CheckUsernameExistsAsync(user.Username);
            var isUserAdded = 0;
            try
            {
                if (!isExist)
                {
                    User userEntity = _unitOfWork.MapperModelToDto<UserModel, User>(user);
                    userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

                   await _unitOfWork.Repository<User>().AddAsync(userEntity);
                   isUserAdded = await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
           

            return isUserAdded > 0;
        }

        private async Task<bool> CheckUsernameExistsAsync(string username)
        {
            return await _unitOfWork.CheckUsernameExistsAsync(username);
        }
    }
}
