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
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly ILog _log;
        private readonly IService _service;


        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger, IService service)
        {
            _paymentService = paymentService;
            _log = LogManager.GetLogger(typeof(PaymentController));
            _logger = logger;
            _service = service;
        }
        
        /// <summary>
        ///  Payment details  of customer
        /// </summary>
        /// <param name="payment"></param>
        [HttpPost]
        public IActionResult CreatePayment( [FromBody] PaymentForCreatingDto payment)
        {
            _logger.LogInformation("Creating Payment");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Enter Valid  data");
                ErrorDto badRequest = _service.ModelStateInvalid(ModelState);
                return BadRequest(badRequest);
            }
            try
            {
                ResponseDto addressId = _paymentService.CreateUserPayment(payment);

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
        /// Getting payment details  of customer
        /// </summary>
      
        [HttpGet]
        public IActionResult GetPaymentByUserId()
        {
            _logger.LogInformation("Getting payment deatils...");

            try
            {
                List<PaymentDto> userAddress = _paymentService.GetPaymentByUserId();
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
        /// Gets an payment by payment id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paymentId"></param>

        [HttpGet]
        [Route("{paymentId}")]
        public IActionResult GetpaymentBypaymentId([FromRoute] Guid paymentId)
        {
            _logger.LogInformation("Getting payment...");
           PaymentDto userpayment = _paymentService.GetpaymentById(paymentId);
            _logger.LogInformation("Getting payment...Completed");
            return StatusCode(StatusCodes.Status200OK, userpayment);
        }
        [HttpPut]
        [Route("{paymentId}")]
        public IActionResult UpdatePaymentById( [FromRoute] Guid paymentId,[FromBody] PaymentUpdateDto updatedPayment)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Enter Valid  data");
                ErrorDto badRequest = _service.ModelStateInvalid(ModelState);
                return BadRequest(badRequest);
            }
            _paymentService.UpdatePaymentById(paymentId,updatedPayment);

            return StatusCode(StatusCodes.Status200OK);
        }
        /// <summary>
        /// Deletes an payment by userid
        /// </summary>
        [HttpDelete]

        public IActionResult DeleteByUserId()
        {
            try
            {
                _paymentService.DeleteByUserId();

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
        /// Deletes an payment by payment id
        /// </summary>
        /// <param name="paymentId"></param>
        [HttpDelete]
        [Route("{userId}/{paymentId}")]
        public IActionResult DeleteByPaymentId([FromRoute] Guid paymentId)
        {
            try
            {
              _paymentService.DeleteByPaymentId(paymentId);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (NotFoundException e)
            {
                _logger.LogDebug("No payment with this user Id found in the database");
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
