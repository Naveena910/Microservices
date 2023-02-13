using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
   public interface IUnitOfWork : IDisposable
    {
        IUserRepository userg { get; }
        IAddressRepository addresss { get; }
        IPaymentRepository payment { get; }
        void Save();

        public void CurrentUser(Guid userId);
        public void DeleteAllAddress(Guid userId);
        /// <summary>
        /// Deletes all the payment with this user Id
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteAllPayment(Guid userId);
         public Guid GetuserId();

    }
}
