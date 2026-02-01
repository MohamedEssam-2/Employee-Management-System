using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo_DAL.Models.Shared;

namespace Demo_DAL.Data.Repositories.Interfaces
{
    public interface IGeneric_Repository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withtracking = false);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate);
        IEnumerable<TResult>GetAll<TResult>(Expression <Func<TEntity,TResult>> Selector);
        TEntity? GetById(int id);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        //IEnumerable<TEntity> GetALLIEnumerable();
        //IQueryable<TEntity> GetALLIQueryable();

    }
}
