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
    public class ProductTypeRepository : IProductTypeRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<ProductType> All
        {
            get { return context.ProductTypes; }
        }

        public IQueryable<ProductType> AllIncluding(params Expression<Func<ProductType, object>>[] includeProperties)
        {
            IQueryable<ProductType> query = context.ProductTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ProductType Find(System.Guid id)
        {
            return context.ProductTypes.Find(id);
        }

        public void InsertOrUpdate(ProductType producttype)
        {
            if (producttype.Id == default(System.Guid)) {
                // New entity
                producttype.Id = Guid.NewGuid();
                context.ProductTypes.Add(producttype);
            } else {
                // Existing entity
                context.Entry(producttype).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var producttype = context.ProductTypes.Find(id);
            context.ProductTypes.Remove(producttype);
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

    public interface IProductTypeRepository : IDisposable
    {
        IQueryable<ProductType> All { get; }
        IQueryable<ProductType> AllIncluding(params Expression<Func<ProductType, object>>[] includeProperties);
        ProductType Find(System.Guid id);
        void InsertOrUpdate(ProductType producttype);
        void Delete(System.Guid id);
        void Save();
    }
}