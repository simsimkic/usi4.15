using Hospital.Model;
using Hospital.Model.Service;
using Hospital.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.Controller
{
    public class OrderController
    {
        private OrderDAO _orderDAO;
        private OrderService _orderService;



        public OrderController(EquipmentDAO equipmentDAO,RoomDAO roomDAO)
        {
            _orderDAO = new OrderDAO();
            _orderService = new OrderService(_orderDAO,equipmentDAO,roomDAO);

            _orderService.CheckOrders();
        }

        public void Create(Order order)
        {
            _orderDAO.Add(order);
        }

        public OrderDAO GetDAO()
        {
           return _orderDAO;
        }
    }


  
}
