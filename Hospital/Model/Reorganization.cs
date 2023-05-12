using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Reorganization
    {
        public string from { get; set; }   
         public string to { get; set; }
        public string equipment { get; set; }
        public int quantity { get; set; }
        public DateTime dueDate { get; set; }

        public Reorganization()
        {
        }
        public Reorganization(string from, string to, int quantity, DateTime date,string equipment)
        {
            this.from = from;
            this.to = to;
            this.quantity = quantity;
            this.dueDate = date;
            this.equipment = equipment;
        }

        public override string? ToString()
        {
            return " 'from:"+from+", to:"+to+", equipment:"+equipment+", quantity:"+quantity+"' ";
        }
    }
}
