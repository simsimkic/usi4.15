using Hospital.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model.DAO
{
    public class OrderDAO
    {

        public List<Order> orders;

        private OrderStorage _storage;

        public OrderDAO()
        {
            _storage = new OrderStorage();
            orders = _storage.Load();

        }

        public List<Order> GetAll()
        {
            return orders;
        }

        public void Add(Order order)
        {
            orders.Add(order);
            _storage.Save(orders);

        }



        public void Remove(Order order)
        {
            orders.Remove(order);
            _storage.Save(orders);
        }

       


    }
}
