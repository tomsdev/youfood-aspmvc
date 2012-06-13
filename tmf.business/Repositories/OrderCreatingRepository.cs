using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using tmf.entities;
using tmf.business;

namespace tmf.web.Models
{ 
    public class OrderCreatingRepository : IOrderCreatingRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<OrderCreating> All
        {
            get { return context.OrderCreatings; }
        }

        public IQueryable<OrderCreating> AllIncluding(params Expression<Func<OrderCreating, object>>[] includeProperties)
        {
            IQueryable<OrderCreating> query = context.OrderCreatings;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderCreating Find(System.Guid id)
        {
            return context.OrderCreatings.Find(id);
        }

        public void InsertOrUpdate(OrderCreating ordercreating)
        {
            if (ordercreating.Id == default(System.Guid)) {
                // New entity
                ordercreating.Id = Guid.NewGuid();
                context.OrderCreatings.Add(ordercreating);
            } else {
                // Existing entity
                context.Entry(ordercreating).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var ordercreating = context.OrderCreatings.Find(id);
            context.OrderCreatings.Remove(ordercreating);
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

    public interface IOrderCreatingRepository : IDisposable
    {
        IQueryable<OrderCreating> All { get; }
        IQueryable<OrderCreating> AllIncluding(params Expression<Func<OrderCreating, object>>[] includeProperties);
        OrderCreating Find(System.Guid id);
        void InsertOrUpdate(OrderCreating ordercreating);
        void Delete(System.Guid id);
        void Save();
    }
}