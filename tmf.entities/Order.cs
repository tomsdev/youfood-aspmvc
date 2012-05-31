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
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// Clients table number
        /// </summary>
        [Required]
        public int Table { get; set; }

        ///// <summary>
        ///// Current state of the order
        ///// </summary>
        //[Required]
        //public int State { get; set; }

        ///// <summary>
        ///// Current state of the order
        ///// </summary>
        //[NotMapped]
        //[ScaffoldColumn(false)]
        //public OrderState StateProp
        //{
        //    get {
        //        return (OrderState)State;
        //    }
        //    set {

        //    }
        //}

        [NotMapped]
        [ScaffoldColumn(false)]
        public OrderState State { get; set; }

        [Required]
        public int StateValue { get { return (int)State; } set { State = (OrderState)value; } }

        // Relations

        [HiddenInput(DisplayValue = false)]
        public Guid WaiterId { get; set; }

        /// <summary>
        /// Responsible waiter
        /// </summary>
        public Waiter Waiter { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        /// <summary>
        /// Menus placed in the order
        /// </summary>
        public virtual ICollection<Menu> Menus { get; set; }

        public override string ToString()
        {
            //ex: Table 3 (Paid)
            return "Table " + Table + " (" + State.ToString() + ")";
        }
    }

}
