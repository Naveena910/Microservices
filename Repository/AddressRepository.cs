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
    public class AddressRepository
        : GenericRepository<Address>, IAddressRepository
    {
        protected readonly RepositoryContext _context;
        public AddressRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
