using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        public bool GetUser(Guid userId,Guid paymentId);
    }
}
