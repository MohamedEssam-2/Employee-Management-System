using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo_DAL.Data.Context;
using Demo_DAL.Data.Repositories.Interfaces;
using Demo_DAL.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace Demo_DAL.Data.Repositories.Classes
{
    public class Generic_Repository<TEntity> (ApplicationDbContext _context): IGeneric_Repository<TEntity> where TEntity : BaseEntity
    {
        public TEntity? GetById(int id)
        {

            var entity = _context.Set<TEntity>().Find(id);
            return entity;
        }


        public IEnumerable<TEntity> GetAll(bool withtracking = false)
        {
            if (withtracking)
                return _context.Set<TEntity>().Where(entity=>entity.IsDeleted==false).ToList();
            else
                return _context.Set<TEntity>().AsNoTracking().Where(entity => entity.IsDeleted == false).ToList();
        }
        
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
           
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
          
        }
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
           
        }

        //IQueryable =>Deferred Execution
        //IEnumerable=> Immediate Execution Use(.toList(),or any collection at the end of query to execute Immediately)
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> Selector)
        {
            return _context.Set<TEntity>()//// returns DbSet<TEntity> => implements IQueryable<TEntity>
            .Where(entity => entity.IsDeleted == false)
            .Select(Selector).ToList();//= implements IEnumerable
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)
        {
            return _context.Set<TEntity>().Where(Predicate).Where(isdeleted=>isdeleted.IsDeleted==false).ToList();
        }


        //public IEnumerable<TEntity> GetALLIEnumerable()
        //{
        //    return _context.Set<TEntity>();
        //}

        //public IQueryable<TEntity> GetALLIQueryable()
        //{
        //    return _context.Set<TEntity>();
        //}
    }
}
