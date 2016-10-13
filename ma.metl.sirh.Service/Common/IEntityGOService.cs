using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IEntityGOService<T> : IService
     where T : class
    {
        void Create(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll();
        void Update(T entity);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T FindById(int id);
    }
}
