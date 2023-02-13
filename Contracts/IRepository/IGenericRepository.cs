using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
          IEnumerable<T>  GetAll();
       T GetById(Guid id);
        void Add(T entity);
       void Update(T entity);
        void Delete(T entity);
        public IEnumerable<T> GetAllById(Expression<Func<T, bool>> expression);
       
    }
}
