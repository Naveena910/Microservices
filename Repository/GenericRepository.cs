using Contracts.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RepositoryContext _dbContext;

        public GenericRepository(RepositoryContext context)
        {
            _dbContext = context;
        }

        public T GetById(Guid id)
        {
            return  _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return  _dbContext.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }


        public void Delete(T entity)
        {

            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
        public IEnumerable<T> GetAllById(Expression<Func<T, bool>> expression) =>
           _dbContext.Set<T>().Where(expression).AsNoTracking();
       
    }
}

