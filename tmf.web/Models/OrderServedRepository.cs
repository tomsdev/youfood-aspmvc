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
    public class OrderServedRepository : IOrderServedRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<OrderServed> All
        {
            get { return context.OrderServeds; }
        }

        public IQueryable<OrderServed> AllIncluding(params Expression<Func<OrderServed, object>>[] includeProperties)
        {
            IQueryable<OrderServed> query = context.OrderServeds;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderServed Find(System.Guid id)
        {
            return context.OrderServeds.Find(id);
        }

        public void InsertOrUpdate(OrderServed orderserved)
        {
            if (orderserved.Id == default(System.Guid)) {
                // New entity
                orderserved.Id = Guid.NewGuid();
                context.OrderServeds.Add(orderserved);
            } else {
                // Existing entity
                context.Entry(orderserved).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var orderserved = context.OrderServeds.Find(id);
            context.OrderServeds.Remove(orderserved);
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

    public interface IOrderServedRepository : IDisposable
    {
        IQueryable<OrderServed> All { get; }
        IQueryable<OrderServed> AllIncluding(params Expression<Func<OrderServed, object>>[] includeProperties);
        OrderServed Find(System.Guid id);
        void InsertOrUpdate(OrderServed orderserved);
        void Delete(System.Guid id);
        void Save();
    }
}