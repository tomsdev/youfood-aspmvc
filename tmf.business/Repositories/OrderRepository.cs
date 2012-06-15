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
    public class OrderRepository : IOrderRepository
    {
        tmfwebContext context = new tmfwebContext();

        public Order TransformOrderTo<T>(Guid idOrder)
             where T : Order, new()
        {
            var order = this.Find(idOrder);

            var newOrder = new T();

            newOrder.Table = order.Table;

            // soit
            newOrder.Menus = order.Menus;

            // peut etre
            //newOrder.Menus = new List<Menu>();

            // soit
            //foreach (var menu in order.Menus)
            //{
            //    newOrder.Menus.Add(menu);
            //}

            newOrder.Restaurant = order.Restaurant;
            newOrder.Waiter = order.Waiter;

            this.InsertOrUpdate(newOrder);
            this.Save();

            this.Delete(idOrder);
            this.Save();

            return newOrder;
        }

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
            var query = from order in context.Orders
                        where order.Id == id
                        select order;

            query = query.Include(p => p.Menus);
            query = query.Include(p => p.Restaurant);
            query = query.Include(p => p.Waiter);

            return query.FirstOrDefault();
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
        Order TransformOrderTo<T>(Guid idOrder) where T : Order, new();
        IQueryable<Order> All { get; }
        IQueryable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties);
        Order Find(System.Guid id);
        void InsertOrUpdate(Order order);
        void Delete(System.Guid id);
        void Save();
    }
}