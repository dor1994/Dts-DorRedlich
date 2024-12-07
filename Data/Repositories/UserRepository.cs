using Data.DtoModels;
using Data.Enums;
using Data.Models;
using Data.Repositories.Interfaces;

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
            try
            {

                if (userEntity == null)
                {
                    return (EnumResponse.UserNotFound, userEntity);
                }

                else if (userEntity != null && !BCrypt.Net.BCrypt.Verify(user.Password, userEntity.PasswordHash))
                    return (EnumResponse.WorngPassword, userEntity);
            }
            catch (Exception e)
            {

                throw e; //Write the error to logger
            }
          
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
                    User userEntity = _unitOfWork.Mapper<UserModel, User>(user);
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
