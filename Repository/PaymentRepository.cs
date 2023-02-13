using Contracts.IRepository;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly RepositoryContext _context;
        public PaymentRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
        public bool GetUser(Guid userId,Guid paymentId)
        {
           return _context.Payment.Select(x=>x.UserId==userId && x.Id==paymentId).FirstOrDefault();
        }
    }
}
