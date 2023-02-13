using Contracts.IServices;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Helpers;
using System.Net;

namespace User_Service.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _AddressService;
        private readonly ILogger<AddressController> _logger;
        private readonly ILog _log;
        private readonly IService _service;


        public AddressController(IAddressService AddressService, ILogger<AddressController> logger, IService service)
        {
            _AddressService = AddressService;
            _log = LogManager.GetLogger(typeof(AddressController));
            _logger = logger;
            _service = service;
        }

        /// <summary>
        ///  delivery address of customer
        /// </summary>
        /// <param name="address"></param>
        [HttpPost]
       
        public IActionResult CreateAddress( [FromBody] AddressForCreatingDto address)
        {
            _logger.LogInformation("Creating addresss...");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Enter Valid  data");
                ErrorDto badRequest = _service.ModelStateInvalid(ModelState);
                return BadRequest(badRequest);
            }
            try
            {
                ResponseDto addressId = _AddressService.CreateUserAddress(address);

                return StatusCode(StatusCodes.Status201Created, addressId);
            }
            catch (UnauthorizedException e)
            {
                _logger.LogDebug("Unauthorized");
                return Conflict(new ErrorDto { ErrorMessage = "Unauthorized", StatusCode = (int)HttpStatusCode.Unauthorized, Description = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }

        }
        /// <summary>
        /// Getting delivery address of customer
        /// </summary>
     
        [HttpGet]
        public IActionResult GetAddressByUserId()
        {
            _logger.LogInformation("Getting addresss...");

            try
            {
                List<AddressDto> userAddress = _AddressService.GetAddressByUserId();
                if (userAddress.Count() == 0)
                {
                    _logger.LogDebug("No address found with this user Id.");
                    return StatusCode(StatusCodes.Status204NoContent, new ErrorDto { ErrorMessage = "No Content", StatusCode = (int)HttpStatusCode.NoContent, Description = "No address found with this user Id" });
                }
                _logger.LogInformation("Getting addresss...Completed");
                return StatusCode(StatusCodes.Status200OK, userAddress);
            }
            catch (UnauthorizedException e)
            {
                _logger.LogDebug("Unauthorized");
                return Unauthorized(new ErrorDto { ErrorMessage = "Unauthorized", StatusCode = (int)HttpStatusCode.Unauthorized, Description = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }

        }
        /// <summary>
        /// Gets an address by address id
        /// </summary>
        /// <param name="addressId"></param>

        [HttpGet]
        [Route("{addressId}")]
        public IActionResult GetAddressByAddressId( [FromRoute] Guid addressId)
        {
            _logger.LogInformation("Getting addresss...");
            AddressDto userAddress = _AddressService.GetAddressById( addressId);
            _logger.LogInformation("Getting addresss...Completed");
            return StatusCode(StatusCodes.Status200OK, userAddress);
        }
        [HttpPut]
        [Route("{addressId}")]
        public IActionResult UpdateAddressById([FromRoute] Guid addressId, AddressUpdateDto updatedAddress)
        {
            _AddressService.UpdateAddressById( addressId, updatedAddress);

            return StatusCode(StatusCodes.Status200OK);
        }
        /// <summary>
        /// Deletes an address by address id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        [HttpDelete]
        [Route("{addressId}")]
        public IActionResult DeleteByAddressId( [FromRoute] Guid addressId)
        {
            try
            {
                _AddressService.DeleteByAddressId(addressId);

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
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }

        }
        /// <summary>
        /// Deletes an address by address id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        [HttpDelete]
        public IActionResult DeleteByUserId()
        {
            try
            {
                _AddressService.DeleteByUserId();

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
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }

        }
        
    }
}