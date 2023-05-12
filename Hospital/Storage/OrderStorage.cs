using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Serializer;
using Hospital.Model;

namespace Hospital.Storage
{
    class OrderStorage
    {

        private const string StoragePath = "../../../Data/orders.json";

        private Serializer<Order> _serializer;


        public OrderStorage()
        {
            _serializer = new Serializer<Order>();
        }

        public List<Order> Load()
        {

            return _serializer.FromJSON(StoragePath);
        }

        public void Save(List<Order>orders)
        {
            _serializer.ToJSON(StoragePath, orders);
        }


    }
}
