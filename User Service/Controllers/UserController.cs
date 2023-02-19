using Contracts.IServices;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Services;
using Services.Helpers;
using System.Net;

namespace User_Service.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IService _service;


        public UserController(IUserService userService, ILogger<UserController>logger,IService service )
        {
            _userService = userService;
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Creates a user account as a customer/admin
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUserAccount([FromBody] UserForCreatingDto user)
        {
            _logger.LogInformation("Creating user in the database");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid data");
                ErrorDto badRequest =_service.ModelStateInvalid(ModelState);
                return BadRequest(badRequest);
            }
            try
            {
                ResponseDto Id = _userService.CreateUser(user);
                _logger.LogInformation("Your account created successfully");
                return Created("User account created", Id);

            }
            catch (ConflictException e)
            {
                _logger.LogDebug("User with Email/phone already exists in the database");
                return Conflict(new ErrorDto { ErrorMessage="Conflict",StatusCode= (int)HttpStatusCode.Conflict, Description=e.Message});
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }
                
           
        }
        /// <summary>
        /// Authenticates the user using email and phone
        /// </summary>
        /// <param name="LogIn"></param>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto LogIn)
        {
                _logger.LogInformation("User checking in controller");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Enter valid Email/password");
                ErrorDto badRequest = _service.ModelStateInvalid(ModelState);
                return BadRequest(badRequest);
            }
            try
            {
                TokenDto accessToken = _userService.Login(LogIn);
                _logger.LogInformation("Login Sucessfulll");
                return Ok(accessToken);
            }
            catch (UnauthorizedException e)
            {
                _logger.LogDebug("Unauthorized");
                return Unauthorized(new ErrorDto { ErrorMessage = "Unauthorized", StatusCode = (int)HttpStatusCode.Unauthorized, Description = e.Message });
            }
            catch (Exception)
            {
                _logger.LogError("Something went  wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }



        }
        /// <summary>
        /// Get the complete user account details by user id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
       // [AllowAnonymous]
        [Route("{id:guid}")]
        public IActionResult GetUserById([FromRoute] Guid id)
        {
            try
            {
                UserDto user = _userService.GetUserById(id);
                _logger.LogInformation("Getting user account with user id");
                return Ok(user);
            }
            catch (UnauthorizedException e)
            {
                _logger.LogDebug("Unauthorized");
                return Unauthorized(new ErrorDto { ErrorMessage = "Unauthorized", StatusCode = (int)HttpStatusCode.Unauthorized, Description = e.Message });
            }
            catch (NotFoundException e)
            {
                _logger.LogDebug("No user with this user Id found in the database");
                return NotFound(new ErrorDto { ErrorMessage = "Notfound", StatusCode = (int)HttpStatusCode.NotFound, Description = e.Message });
            }
            catch (Exception)
            {
                _logger.LogError("Something went  wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }

        }
        /// <summary>
        /// Admins can get all the user account details 
        /// </summary>
       
        [HttpGet]
        public IActionResult GetAllUser()
        {
            try
            {
                List<UserResponseDto> users = _userService.GetAllUser();

                if (users.Count == 0)
                {
                    _logger.LogDebug("No users found");
                    return StatusCode(StatusCodes.Status204NoContent, new ErrorDto { ErrorMessage = "No Content", StatusCode = (int)HttpStatusCode.NoContent, Description = "No address found with this user Id" });
                }
                _logger.LogInformation("Getting all user account ");
                return Ok(users);
            }
            catch (ForbiddenException )

            {
                _logger.LogDebug("No user with this user Id found in the database");
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorDto { ErrorMessage = "Forbidden", StatusCode = (int)HttpStatusCode.Forbidden, Description = "Access denied" });
            }
            catch (Exception)
            {
                _logger.LogError("Something went  wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }

        }
        /// <summary>
        /// Update  user account details by user id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateDto user)
        {
            try
            {
                _userService.UpdateUser(id, user);
                _logger.LogInformation("Updated user account with user id");
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (UnauthorizedException e)
            {
                _logger.LogDebug("Unauthorized");
                return Unauthorized(new ErrorDto { ErrorMessage = "Unauthorized", StatusCode = (int)HttpStatusCode.Unauthorized, Description = e.Message });
            }
            catch (ConflictException e)
            {
                _logger.LogDebug("User with Email/phone already exists in the database");
                return Conflict(new ErrorDto { ErrorMessage = "Conflict", StatusCode = (int)HttpStatusCode.Conflict, Description = e.Message });
            }
            catch (Exception)
            {
                _logger.LogError("Something went  wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }
        }
        /// <summary>
        /// Deletes an address by address id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteByUserId([FromRoute] Guid id)
        {
            try
            {
                _userService.DeleteByUserId(id);
                _logger.LogInformation("Deleted user account with user id");
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (NotFoundException e)
            {
                _logger.LogDebug("No addres with this user Id found in the database");
                return NotFound(new ErrorDto { ErrorMessage = "Notfound", StatusCode = (int)HttpStatusCode.NotFound, Description = e.Message });
            }
            catch (UnauthorizedException e)
            {
                _logger.LogDebug("Unauthorized");
                return Unauthorized(new ErrorDto { ErrorMessage = "Unauthorized", StatusCode = (int)HttpStatusCode.Unauthorized, Description = e.Message });
            }
            catch (Exception)
            {
                _logger.LogError("Something went  wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }

        }
        [HttpGet]
        [AllowAnonymous]
        [Route("interservice/{userId}")]
        public IActionResult   GetUserId([FromRoute]Guid userId)
        {
           bool usercheck= _userService.GetUser(userId);
           if(usercheck==true) return Ok();
           return NotFound();
        }

    }




    }
