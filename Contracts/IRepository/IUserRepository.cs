using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IUserRepository :IGenericRepository<User>
    {
        /// <summary>
        /// Checks if an user account already exists in the database
        /// </summary>
        /// <param name="email"></param>
         /// <param name="phone"></param>
        /// <returns></returns>
        public bool UserExists(string email, long phone);
        /// <summary>
        /// Gets user id for the given email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Guid GetUserId(string email);
        public bool GetUser(Guid userId);
        public string GetPassword(string email);

    
    }
}
