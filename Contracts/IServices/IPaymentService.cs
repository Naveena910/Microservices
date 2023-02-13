using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IServices
{
   public interface  IPaymentService
    {
        /// <summary>
        /// creates payment details of the user
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public ResponseDto CreateUserPayment( PaymentForCreatingDto payment);
        /// <summary>
        /// Gets all the payment of a user by user id
        /// </summary>
        /// <returns></returns>
        public List<PaymentDto> GetPaymentByUserId();
        /// <summary>
        /// Update by payment id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="updatePayment"></param>
        public void UpdatePaymentById( Guid paymentId, PaymentUpdateDto updatePayment);
        /// <summary>
        /// Get payment of the user by paymentid
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public PaymentDto GetpaymentById( Guid paymentId);
        /// <summary>
        /// Delete  by user id
        /// </summary>
        public void DeleteByUserId();
        /// <summary>
        /// Delete  by payment id
        /// </summary>
        /// <param name="paymentId"></param>
        public void DeleteByPaymentId(Guid paymentId);
        
    }
}
