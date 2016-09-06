using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrdersData.Repository
{
    public interface IEntityBaseRepository<T> where T : class,  new() 
    {
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] IncludedProperties);
        IQueryable<T> GetAll(string IncludeParams = null);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}
