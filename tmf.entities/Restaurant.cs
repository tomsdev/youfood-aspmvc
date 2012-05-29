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
    /// A YouFood restaurant entity
    /// </summary>
    public class Restaurant
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Relations

        public virtual ICollection<Order> Orders { get; set; }
    }

}
