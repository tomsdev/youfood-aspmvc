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
    public class OrderPaidRepository : IOrderPaidRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<OrderPaid> All
        {
            get { return context.OrderPaids; }
        }

        public IQueryable<OrderPaid> AllIncluding(params Expression<Func<OrderPaid, object>>[] includeProperties)
        {
            IQueryable<OrderPaid> query = context.OrderPaids;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderPaid Find(System.Guid id)
        {
            return context.OrderPaids.Find(id);
        }

        public void InsertOrUpdate(OrderPaid orderpaid)
        {
            if (orderpaid.Id == default(System.Guid)) {
                // New entity
                orderpaid.Id = Guid.NewGuid();
                context.OrderPaids.Add(orderpaid);
            } else {
                // Existing entity
                context.Entry(orderpaid).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var orderpaid = context.OrderPaids.Find(id);
            context.OrderPaids.Remove(orderpaid);
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

    public interface IOrderPaidRepository : IDisposable
    {
        IQueryable<OrderPaid> All { get; }
        IQueryable<OrderPaid> AllIncluding(params Expression<Func<OrderPaid, object>>[] includeProperties);
        OrderPaid Find(System.Guid id);
        void InsertOrUpdate(OrderPaid orderpaid);
        void Delete(System.Guid id);
        void Save();
    }
}