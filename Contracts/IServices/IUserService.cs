using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IServices
{
    public interface IUserService
    {
        /// <summary>
        /// Create a user account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseDto CreateUser(UserForCreatingDto user);
        /// <summary>
        /// Authenticates the user using email and phone
        /// </summary>
        /// <param name="LogIn"></param>
        /// <returns></returns>
        public TokenDto Login(LoginDto user);
        /// <summary>
        /// Gets the user deatils by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserDto GetUserById(Guid userId);
        /// <summary>
        /// Get all user deatils by Admin
        /// </summary>
        /// <returns></returns>

        public List<UserResponseDto> GetAllUser();
        /// <summary>
        /// Update by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userUpdate"></param>
        public void UpdateUser(Guid userId, UserUpdateDto userUpdate);
        /// <summary>
        /// Delete  by user id
        /// </summary>
        /// <param name="userId"></param>

        public void DeleteByUserId(Guid userId);
        public bool GetUser(Guid userId);


    }
}
