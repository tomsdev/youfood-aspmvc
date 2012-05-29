using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tmf.entities
{
    public enum OrderState
    {
        Created, // zone responsible waiter should be informed
        Placed, // cooks should be informed
        Cooking, // a cook is cooking it
        Cooked, // responsible waiter should be informed
        Served, // clients can pay
        Paid // end
    }
}
