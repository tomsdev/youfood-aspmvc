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
    /// <summary>
    /// A dining room is divided into zones, each zones containing seats and tables 
    /// which are placed under the responsibility of a waiter.
    /// </summary>
    public class Zone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Relations

        [HiddenInput(DisplayValue = false)]
        public Guid WaiterId { get; set; }

        /// <summary>
        /// Responsible waiter
        /// </summary>
        public Waiter Waiter { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
