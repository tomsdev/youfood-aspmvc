using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using tmf.entities;

namespace tmf.business.repositories
{ 
    public class MenuRepository : IMenuRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<Menu> All
        {
            get { return context.Menus; }
        }

        public IQueryable<Menu> AllIncluding(params Expression<Func<Menu, object>>[] includeProperties)
        {
            IQueryable<Menu> query = context.Menus;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Menu Find(System.Guid id)
        {
            return context.Menus.Find(id);
        }

        public void InsertOrUpdate(Menu menu)
        {
            if (menu.Id == default(System.Guid)) {
                // New entity
                menu.Id = Guid.NewGuid();
                context.Menus.Add(menu);
            } else {
                // Existing entity
                context.Entry(menu).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var menu = context.Menus.Find(id);
            context.Menus.Remove(menu);
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

    public interface IMenuRepository : IDisposable
    {
        IQueryable<Menu> All { get; }
        IQueryable<Menu> AllIncluding(params Expression<Func<Menu, object>>[] includeProperties);
        Menu Find(System.Guid id);
        void InsertOrUpdate(Menu menu);
        void Delete(System.Guid id);
        void Save();
    }
}