using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eShop.DataAccess.EntityFramework.Context;
using eShop.Domain;
using eShop.Domain.Repositories;
using EnsureThat;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace eShop.DataAccess.EntityFramework.Repository
{
    public abstract class EFRepo<TEntity> : IRepository<TEntity> where TEntity: Entity, new()
    {
        protected readonly OnlineShopContext Context;

        protected EFRepo(OnlineShopContext context)
        {
            EnsureArg.IsNotNull(context);
            Context = context;
        }

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            TEntity entity = new TEntity{Id = id};
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }

        public ICollection<TEntity> Find(Func<TEntity, bool> filter)
        {
            return Context.Set<TEntity>().Where(filter).ToList();
        }

        public ICollection<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();
        }
    }
}
