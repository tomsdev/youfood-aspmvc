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
    public class OrderCookingRepository : IOrderCookingRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<OrderCooking> All
        {
            get { return context.OrderCookings; }
        }

        public IQueryable<OrderCooking> AllIncluding(params Expression<Func<OrderCooking, object>>[] includeProperties)
        {
            IQueryable<OrderCooking> query = context.OrderCookings;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderCooking Find(System.Guid id)
        {
            return context.OrderCookings.Find(id);
        }

        public void InsertOrUpdate(OrderCooking ordercooking)
        {
            if (ordercooking.Id == default(System.Guid)) {
                // New entity
                ordercooking.Id = Guid.NewGuid();
                context.OrderCookings.Add(ordercooking);
            } else {
                // Existing entity
                context.Entry(ordercooking).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var ordercooking = context.OrderCookings.Find(id);
            context.OrderCookings.Remove(ordercooking);
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

    public interface IOrderCookingRepository : IDisposable
    {
        IQueryable<OrderCooking> All { get; }
        IQueryable<OrderCooking> AllIncluding(params Expression<Func<OrderCooking, object>>[] includeProperties);
        OrderCooking Find(System.Guid id);
        void InsertOrUpdate(OrderCooking ordercooking);
        void Delete(System.Guid id);
        void Save();
    }
}