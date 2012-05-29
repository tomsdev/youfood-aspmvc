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
    public class ZoneRepository : IZoneRepository
    {
        tmfwebContext context = new tmfwebContext();

        public IQueryable<Zone> All
        {
            get { return context.Zones; }
        }

        public IQueryable<Zone> AllIncluding(params Expression<Func<Zone, object>>[] includeProperties)
        {
            IQueryable<Zone> query = context.Zones;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Zone Find(System.Guid id)
        {
            return context.Zones.Find(id);
        }

        public void InsertOrUpdate(Zone zone)
        {
            if (zone.Id == default(System.Guid)) {
                // New entity
                zone.Id = Guid.NewGuid();
                context.Zones.Add(zone);
            } else {
                // Existing entity
                context.Entry(zone).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var zone = context.Zones.Find(id);
            context.Zones.Remove(zone);
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

    public interface IZoneRepository : IDisposable
    {
        IQueryable<Zone> All { get; }
        IQueryable<Zone> AllIncluding(params Expression<Func<Zone, object>>[] includeProperties);
        Zone Find(System.Guid id);
        void InsertOrUpdate(Zone zone);
        void Delete(System.Guid id);
        void Save();
    }
}