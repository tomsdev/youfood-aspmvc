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


        public Guid FindIdByName(string zoneName)
        {
            var queryString = from zone in this.All
                              where zone.Name.Equals(zoneName)
                              select zone.Id;

            return queryString.First();
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

        public Zone GetZoneByTable(int table)
        {
            if (table > 0 && table < 10)
            {
                return this.Find(FindIdByName("Zone1"));                
            }
            else if (table >= 10 && table < 20)
            {
                return this.Find(FindIdByName("Zone2"));
            }
            else if (table >= 20 && table < 30)
            {
                return this.Find(FindIdByName("Zone3"));
            }
            else if (table >= 30 && table < 40)
            {
                return this.Find(FindIdByName("Zone4"));
            }
            else if (table >= 40 && table < 50)
            {
                return this.Find(FindIdByName("Zone5"));
            }
            else
            {
                return new Zone();
            }
        }

    }

    public interface IZoneRepository : IDisposable
    {
        IQueryable<Zone> All { get; }
        IQueryable<Zone> AllIncluding(params Expression<Func<Zone, object>>[] includeProperties);
        Guid FindIdByName(string zoneName);
        Zone Find(System.Guid id);
        Zone GetZoneByTable(int table);
        void InsertOrUpdate(Zone zone);
        void Delete(System.Guid id);
        void Save();
    }
}