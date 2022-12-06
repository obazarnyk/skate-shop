
using System;
using educational_practice5.Repositories.Interfaces;

using System.Collections.Generic;
using educational_practice5.Database;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace educational_practice5.Repositories.Implimentations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }
        public BaseRepository(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }
        
        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext .Set<T>().AsNoTracking();


        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext .Set<T>().Where(expression).AsNoTracking();


        }
        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
            this.RepositoryContext.SaveChanges();
        }
        public void Update(T entity)
        {
            this.RepositoryContext .Set<T>().Update(entity);
            this.RepositoryContext.SaveChanges();

        }
        public void Delete(T entity)
        {
            this.RepositoryContext .Set<T>().Remove(entity);
            this.RepositoryContext.SaveChanges();

        }
    }
}