using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using ma.metl.sirh.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public abstract class EntityGOService<T> : IEntityGOService<T> where T : class
    {
        IUnitOfWork _unitOfWork;
        IGenericRepositoryOrd<T> _repository;

        public EntityGOService(IUnitOfWork unitOfWork, IGenericRepositoryOrd<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }     


        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _repository.Save();
            //TODO: Check why The UnitOfWork.Commit() does not save changes!
            //_unitOfWork.Commit();         
        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Edit(entity);
            _repository.Save();
            //TODO: Check why The UnitOfWork.Commit() does not save changes!
            //_unitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
            _repository.Save();
            //TODO: Check why The UnitOfWork.Commit() does not save changes!
            //_unitOfWork.Commit();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        public virtual T FindById(int id)
        {
            return _repository.FindById(id);
        }
    }
}
