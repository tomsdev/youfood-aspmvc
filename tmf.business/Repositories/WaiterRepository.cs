using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using tmf.entities;

namespace tmf.business.repositories
{ 
    public class WaiterRepository : IWaiterRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<Waiter> All
        {
            get { return context.Waiters; }
        }

        public IQueryable<Waiter> AllIncluding(params Expression<Func<Waiter, object>>[] includeProperties)
        {
            IQueryable<Waiter> query = context.Waiters;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Waiter Find(System.Guid id)
        {
            return context.Waiters.Find(id);
        }

        public void InsertOrUpdate(Waiter waiter)
        {
            if (waiter.Id == default(System.Guid)) {
                // New entity
                waiter.Id = Guid.NewGuid();
                context.Waiters.Add(waiter);
            } else {
                // Existing entity
                context.Entry(waiter).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var waiter = context.Waiters.Find(id);
            context.Waiters.Remove(waiter);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IWaiterRepository : IDisposable
    {
        IQueryable<Waiter> All { get; }
        IQueryable<Waiter> AllIncluding(params Expression<Func<Waiter, object>>[] includeProperties);
        Waiter Find(System.Guid id);
        void InsertOrUpdate(Waiter waiter);
        void Delete(System.Guid id);
        void Save();
    }
}