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
    /// Culture of the product (Chinese, American, etc.)
    /// </summary>
    public class ProductType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        // Relations

        public virtual ICollection<Menu> Menus { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
