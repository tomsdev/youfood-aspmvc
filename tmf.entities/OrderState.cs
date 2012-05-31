using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tmf.entities
{
    public enum OrderState
    {
        [Display(Name = "Created", Order = 0)]
        Created, // zone responsible waiter should be informed

        [Display(Name = "Placed", Order = 1)]
        Placed, // cooks should be informed

        [Display(Name = "Cooking", Order = 2)]
        Cooking, // a cook is cooking it

        [Display(Name = "Cooked", Order = 3)]
        Cooked, // responsible waiter should be informed

        [Display(Name = "Served", Order = 4)]
        Served, // clients can pay

        [Display(Name = "Paid", Order = 5)]
        Paid // end
    }
}
