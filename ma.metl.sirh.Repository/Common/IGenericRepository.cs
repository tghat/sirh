using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;
using System.Data.Entity;
namespace ma.metl.sirh.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        DbContext getEntities();
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
