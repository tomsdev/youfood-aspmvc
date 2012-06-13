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
    public class OrderCookedRepository : IOrderCookedRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<OrderCooked> All
        {
            get { return context.OrderCookeds; }
        }

        public IQueryable<OrderCooked> AllIncluding(params Expression<Func<OrderCooked, object>>[] includeProperties)
        {
            IQueryable<OrderCooked> query = context.OrderCookeds;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderCooked Find(System.Guid id)
        {
            return context.OrderCookeds.Find(id);
        }

        public void InsertOrUpdate(OrderCooked ordercooked)
        {
            if (ordercooked.Id == default(System.Guid)) {
                // New entity
                ordercooked.Id = Guid.NewGuid();
                context.OrderCookeds.Add(ordercooked);
            } else {
                // Existing entity
                context.Entry(ordercooked).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var ordercooked = context.OrderCookeds.Find(id);
            context.OrderCookeds.Remove(ordercooked);
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

    public interface IOrderCookedRepository : IDisposable
    {
        IQueryable<OrderCooked> All { get; }
        IQueryable<OrderCooked> AllIncluding(params Expression<Func<OrderCooked, object>>[] includeProperties);
        OrderCooked Find(System.Guid id);
        void InsertOrUpdate(OrderCooked ordercooked);
        void Delete(System.Guid id);
        void Save();
    }
}