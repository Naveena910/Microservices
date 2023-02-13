using Contracts.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext _dbContext;
        public IUserRepository userg { get; }
        public IAddressRepository addresss { get; }
        public IPaymentRepository payment { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(RepositoryContext dbContext,
                            IUserRepository userRepository,
                            IAddressRepository address,IHttpContextAccessor httpContextAccessor,IPaymentRepository paymentRepository)
        {
            _dbContext = dbContext;
            userg = userRepository;
              addresss = address;
            payment= paymentRepository; 
            _httpContextAccessor= httpContextAccessor;
        }

        public void Save()
        {
             _dbContext.SaveChanges();
        }
        public void CurrentUser(Guid userId)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User?.FindFirstValue("userid"));

            if (currentId != userId)
            {
                throw new UnauthorizedException("user not found");
            }


        }
      public Guid GetuserId()
        {
                  return new Guid(_httpContextAccessor.HttpContext.User?.FindFirstValue("userid"));

        }
        /// <summary>
        /// Deletes all the address with this user Id
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteAllAddress(Guid userId)
        {
            var q =
            from user in _dbContext.User
                    join address in _dbContext.Address
                    on user.Id equals address.UserId
                    where user.Id == userId && user.IsActive == true
                    select new
                    {
                        Addresses = address,
                    };
            foreach (var item in q)
            {
                _dbContext.Address.Remove(item.Addresses);
            }

        }
        /// <summary>
        /// Deletes all the payment with this user Id
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteAllPayment(Guid userId)
        {
            var q =
            from user in _dbContext.User
            join payment in _dbContext.Payment
            on user.Id equals payment.UserId
            where user.Id == userId && user.IsActive == true
            select new
            {
                Payments= payment,
            };
            foreach (var item in q)
            {
                _dbContext.Payment.Remove(item.Payments);
            }

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
