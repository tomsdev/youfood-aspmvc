using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using tmf.entities;

namespace tmf.business.repositories
{
    public class OrderRepository : IOrderRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<Order> All
        {
            get { return context.Orders; }
        }

        public IQueryable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties)
        {
            IQueryable<Order> query = context.Orders;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Order Find(System.Guid id)
        {
            return context.Orders.Find(id);
        }

        public void InsertOrUpdate(Order order)
        {
            if (order.Id == default(System.Guid)) {
                // New entity
                order.Id = Guid.NewGuid();
                context.Orders.Add(order);
            } else {
                // Existing entity
                context.Entry(order).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var order = context.Orders.Find(id);
            context.Orders.Remove(order);
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

    public interface IOrderRepository : IDisposable
    {
        IQueryable<Order> All { get; }
        IQueryable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties);
        Order Find(System.Guid id);
        void InsertOrUpdate(Order order);
        void Delete(System.Guid id);
        void Save();
    }
}