using Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

       
        public DbSet<User> User { get; set; }

       

        public DbSet<Address> Address { get; set; }

        public DbSet<Payment> Payment { get; set; }

    }
}

