using Contracts.IRepository;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static Repository.UserRepository;

namespace Repository
{

    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        protected readonly RepositoryContext _context;
        public UserRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
     
        /// <summary>
        /// Checks if an user account already exists in the DB
        /// </summary>
        /// <returns></returns>
        public bool UserExists(string email , long phone)
        {   

            return _context.User.Any(a => (a.Email == email || a.Phone == phone) && a.IsActive == true);
        }
        
        /// <summary>
        /// Gets user id for the given email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Guid GetUserId(string email)
        {
            return _context.User.Where(a => a.Email == email && a.IsActive == true).Select(a => a.Id).SingleOrDefault();
        }
        public string GetPassword(string email)
        {
            return _context.User.Where(a => a.Email ==email).Select(a => a.Password).SingleOrDefault();
        }
        
 public bool GetUser(Guid userId)
        {
           return _context.User.Any(x=>x.Id==userId );
        }
        
    }
}
