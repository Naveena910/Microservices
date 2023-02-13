using AutoMapper;
using Contracts.IRepository;
using Contracts.IServices;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using Entities.Model;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Services
{
    public class UserService :IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IService _service;
        private readonly ILogger<UserService> _logger;
        public IUnitOfWork _unitOfWork;
        public UserService(IMapper mapper, IUserRepository userRepository,
                                  IConfiguration configuration, IService service, ILogger<UserService> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _configuration = configuration;
            _service = service;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create a user account 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// 
        public ResponseDto CreateUser(UserForCreatingDto user)
        {
            _logger.LogInformation("Creating account in service");
            
                if (_userRepository.UserExists(user.Email, user.Phone))
                {
                    _logger.LogDebug("Account already found in the database");
                    throw new ConflictException("User with Email/Phone already exists");
                }
            
                User userAccount = _mapper.Map<User>(user);

                if (userAccount.Address != null)
                {
                    foreach (Address address in userAccount.Address)
                    {
                        address.UserId = userAccount.Id;
                    }
                }
                _unitOfWork.userg.Add(userAccount);
                _unitOfWork.Save();
                _logger.LogInformation("User details added from service");
                return new ResponseDto { Id = userAccount.Id };
            
           
        }

       
        /// <summary>
        /// Authenticates the user email and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TokenDto Login(LoginDto user)
        {
            _logger.LogInformation("User checking in services");
            Guid userId =_userRepository.GetUserId(user.Email);
            if (userId == Guid.Empty)
            {
               _logger.LogDebug("Invalid user credentials");
                throw new UnauthorizedException("Given Email is incorrect");
            }
            string? password = _userRepository.GetPassword(user.Email);
            if (password!=user.Password)
            {
                _logger.LogDebug("Invalid user credentials");
                throw new UnauthorizedException("Given password is incorrect");
            }


            User users= _unitOfWork.userg.GetById(userId);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userid", userId.ToString()),
                    new Claim("UserType", users.UserType),
                    new Claim("first_name", users.FirstName),
                    new Claim("last_name", users.LastName),
                    new Claim("email", users.Email),
                    new Claim("password", password)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _logger.LogInformation("Login sucessfulll in service");
            return new TokenDto { Token = tokenHandler.WriteToken(token) };
        }
        /// <summary>
        /// Gets the user deatils by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public  UserDto GetUserById(Guid userId)
        {
            _logger.LogInformation("Getting user deatils from service");
           // _unitOfWork.CurrentUser(userId);

            User user =  _unitOfWork.userg.GetById(userId);

            if (user == null)
            {
                _logger.LogDebug("User Account Not found");
                throw new NotFoundException("User Not Found");
            }

           IEnumerable<Address> Addresses = _unitOfWork.addresss.GetAllById(x => x.UserId == userId);
           IEnumerable<Payment> Payments = _unitOfWork.payment.GetAllById(x => x.UserId == userId);

            UserDto users = _mapper.Map<UserDto>(user);
            users.Address= _mapper.Map<List<AddressDto>>(Addresses);
            users.Payment = _mapper.Map<List<PaymentDto>>(Payments);

            return users;
        }
        /// <summary>
        /// Get all user deatils by Admin
        /// </summary>
        /// <returns></returns>

        public List<UserResponseDto> GetAllUser()
        {
            _service.Typecheck();
            List<UserResponseDto> users = _mapper.Map<List<UserResponseDto>>(_unitOfWork.userg.GetAll());
            return users;
        }
        /// <summary>
        /// Update by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userUpdate"></param>
        public void UpdateUser(Guid userId,UserUpdateDto userUpdate)
        {
             _unitOfWork.CurrentUser(userId);
             User user = _unitOfWork.userg.GetById(userId);
            if (_userRepository.UserExists(userUpdate.Email, userUpdate.Phone))
            {
                _logger.LogDebug("Account already found in the database");
                throw new ConflictException("User with Email/Phone already exists");
            }
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            user.FirstName = userUpdate.FirstName;
            user.LastName = userUpdate.LastName;
            user.UserType = userUpdate.UserType;
            user.DateUpdated = DateTime.Now;
            user.DateCreated = user.DateCreated;
            user.Email = userUpdate.Email;
            user.Password=userUpdate.Password;
            user.Phone= userUpdate.Phone;
            user.IsActive = true;
            user.Id=userId; 
            _unitOfWork.userg.Update(user);
            _unitOfWork.Save();

        }

        /// <summary>
        /// Delete  by user id
        /// </summary>
        /// <param name="userId"></param>

        public void DeleteByUserId(Guid userId)
        {
          _unitOfWork.CurrentUser(userId);
           User address =_unitOfWork.userg.GetById(userId);

            if (address == null)
            {
                throw new NotFoundException("No address has been found");
            }
            _unitOfWork.userg.Delete(address);
            _unitOfWork.Save();
        }
           public bool GetUser(Guid userId)
       {
        bool s=_userRepository.GetUser(userId);
        if(s==true) return true;
        return false;
       }
    }
}

