using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tmf.entities;

namespace tmf.web.ViewModels
{
    public class OrderOneMenuViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }

        public Guid IdOrder { get; set; }
        public Guid IdMenuSelected { get; set; }
        public bool IsOrderTerminated { get; set; }
    }
}