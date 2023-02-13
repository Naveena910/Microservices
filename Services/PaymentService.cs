using AutoMapper;
using Contracts.IRepository;
using Contracts.IServices;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using Entities.Model;
using log4net;
using Microsoft.Extensions.Logging;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentService : IPaymentService
    {

        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IService _service;
        private readonly ILogger<PaymentService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository,
                                  IService service, ILogger<PaymentService> logger,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _service = service;
            _logger = logger;
            _unitOfWork= unitOfWork;
        }
        /// <summary>
        /// creates payment details of the user
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public ResponseDto CreateUserPayment(PaymentForCreatingDto payment)
        {
            Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
            Payment userPayment = _mapper.Map<Payment>(payment);
            userPayment.UserId = userId;
            _unitOfWork.payment.Add(userPayment);
            _unitOfWork.Save();
            return new ResponseDto { Id = userPayment.Id };

        }
        /// <summary>
        /// Gets all the payment of a user by user id
        /// </summary>
        /// <returns></returns>
        public List<PaymentDto> GetPaymentByUserId()
        {
          Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
            IEnumerable<Payment> userPayment = _unitOfWork.payment.GetAllById(x => x.UserId == userId);

            return _mapper.Map<List<PaymentDto>>(userPayment);

        }
        /// <summary>
        /// Get payment of the user by paymentid
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public PaymentDto GetpaymentById( Guid paymentId)
        {
           Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);

            Payment userpayment = _unitOfWork.payment.GetById(paymentId);

            if (userpayment == null)
            {
                throw new NotFoundException("No payment has been found");
            }
            return _mapper.Map<PaymentDto>(userpayment);

        }
        /// <summary>
        /// Update by payment id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="updatePayment"></param>
        public void UpdatePaymentById( Guid paymentId, PaymentUpdateDto updatePayment)
        {
            Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
            Payment payment = _unitOfWork.payment.GetById(paymentId);

            if (payment == null)
            {
                throw new NotFoundException("Address not found");
            }
            payment.AccountNumber = updatePayment.AccountNumber;
            payment.Expiry = updatePayment.Expiry;
            payment.Type= updatePayment.Type;
           _unitOfWork.payment.Update(payment);
           _unitOfWork.Save();
          
        }

        /// <summary>
        /// Delete  by user id
        /// </summary>

        public void DeleteByUserId()
        {
             Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
            IEnumerable<Payment> userPayment= _unitOfWork.payment.GetAllById(x => x.UserId == userId);

            if (userPayment.Count() == 0)
            {
                throw new NotFoundException("No payment has been found");
            }
            _unitOfWork.DeleteAllPayment(userId);
            _unitOfWork.Save();
        }
        /// <summary>
        /// Delete  by payment id
        /// </summary>
        /// <param name="paymentId"></param>
        public void DeleteByPaymentId(Guid paymentId)
        {
           Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
            Payment payment = _unitOfWork.payment.GetById(paymentId);

            if (payment == null)
            {
                throw new NotFoundException("No payment has been found");
            }
            _unitOfWork.payment.Delete(payment);
            payment.IsActive = false;
            _unitOfWork.Save();
        }
      
    }

}
