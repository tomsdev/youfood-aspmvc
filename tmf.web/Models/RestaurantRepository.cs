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
    public class RestaurantRepository : IRestaurantRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<Restaurant> All
        {
            get { return context.Restaurants; }
        }

        public IQueryable<Restaurant> AllIncluding(params Expression<Func<Restaurant, object>>[] includeProperties)
        {
            IQueryable<Restaurant> query = context.Restaurants;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Restaurant Find(System.Guid id)
        {
            return context.Restaurants.Find(id);
        }

        public void InsertOrUpdate(Restaurant restaurant)
        {
            if (restaurant.Id == default(System.Guid)) {
                // New entity
                restaurant.Id = Guid.NewGuid();
                context.Restaurants.Add(restaurant);
            } else {
                // Existing entity
                context.Entry(restaurant).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var restaurant = context.Restaurants.Find(id);
            context.Restaurants.Remove(restaurant);
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

    public interface IRestaurantRepository : IDisposable
    {
        IQueryable<Restaurant> All { get; }
        IQueryable<Restaurant> AllIncluding(params Expression<Func<Restaurant, object>>[] includeProperties);
        Restaurant Find(System.Guid id);
        void InsertOrUpdate(Restaurant restaurant);
        void Delete(System.Guid id);
        void Save();
    }
}