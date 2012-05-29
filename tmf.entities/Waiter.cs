using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace tmf.entities
{
    public class Waiter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        // Relations

        /// <summary>
        /// Responsible zones
        /// </summary>
        public virtual ICollection<Zone> Zones { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
