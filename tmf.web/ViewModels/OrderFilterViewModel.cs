using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tmf.entities;

namespace tmf.web.ViewModels
{
    public class OrderFilterViewModel
    {
        // GET
        public IQueryable<OrderPaid> Orders { get; set; }

        // POST
        public Guid? ProductTypeId { get; set; }
        public Guid? RestaurantId { get; set; }
    }
}