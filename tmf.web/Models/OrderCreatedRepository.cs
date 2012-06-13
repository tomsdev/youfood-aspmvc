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
    public class OrderCreatedRepository : IOrderCreatedRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<OrderCreated> All
        {
            get { return context.OrderCreateds; }
        }

        public IQueryable<OrderCreated> AllIncluding(params Expression<Func<OrderCreated, object>>[] includeProperties)
        {
            IQueryable<OrderCreated> query = context.OrderCreateds;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderCreated Find(System.Guid id)
        {
            return context.OrderCreateds.Find(id);
        }

        public void InsertOrUpdate(OrderCreated ordercreated)
        {
            if (ordercreated.Id == default(System.Guid)) {
                // New entity
                ordercreated.Id = Guid.NewGuid();
                context.OrderCreateds.Add(ordercreated);
            } else {
                // Existing entity
                context.Entry(ordercreated).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var ordercreated = context.OrderCreateds.Find(id);
            context.OrderCreateds.Remove(ordercreated);
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

    public interface IOrderCreatedRepository : IDisposable
    {
        IQueryable<OrderCreated> All { get; }
        IQueryable<OrderCreated> AllIncluding(params Expression<Func<OrderCreated, object>>[] includeProperties);
        OrderCreated Find(System.Guid id);
        void InsertOrUpdate(OrderCreated ordercreated);
        void Delete(System.Guid id);
        void Save();
    }
}