using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Order
    {   
        public string storageRoom { get; set; }
        public string equipment { get; set; }
        public int quantity { get; set; }
        public DateTime arrivalDate { get; set; }


        public Order() { }

        
        public Order(string storageRoom, string equipment, int quantity, DateTime arrivalDate)
        {
            this.storageRoom = storageRoom;
            this.equipment = equipment;
            this.quantity = quantity;
            this.arrivalDate = arrivalDate;
        }

        public override string ToString()
        {
            return "";
        }

        


    }
}
