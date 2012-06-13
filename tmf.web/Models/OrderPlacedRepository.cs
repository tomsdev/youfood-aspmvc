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
    public class OrderPlacedRepository : IOrderPlacedRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<OrderPlaced> All
        {
            get { return context.OrderPlaceds; }
        }

        public IQueryable<OrderPlaced> AllIncluding(params Expression<Func<OrderPlaced, object>>[] includeProperties)
        {
            IQueryable<OrderPlaced> query = context.OrderPlaceds;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderPlaced Find(System.Guid id)
        {
            return context.OrderPlaceds.Find(id);
        }

        public void InsertOrUpdate(OrderPlaced orderplaced)
        {
            if (orderplaced.Id == default(System.Guid)) {
                // New entity
                orderplaced.Id = Guid.NewGuid();
                context.OrderPlaceds.Add(orderplaced);
            } else {
                // Existing entity
                context.Entry(orderplaced).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var orderplaced = context.OrderPlaceds.Find(id);
            context.OrderPlaceds.Remove(orderplaced);
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

    public interface IOrderPlacedRepository : IDisposable
    {
        IQueryable<OrderPlaced> All { get; }
        IQueryable<OrderPlaced> AllIncluding(params Expression<Func<OrderPlaced, object>>[] includeProperties);
        OrderPlaced Find(System.Guid id);
        void InsertOrUpdate(OrderPlaced orderplaced);
        void Delete(System.Guid id);
        void Save();
    }
}