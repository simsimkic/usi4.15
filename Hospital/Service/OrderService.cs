using Hospital.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model.Service
{
    public class OrderService
    {
        OrderDAO orderDAO;
        EquipmentDAO equipmentDAO;
        RoomDAO roomDAO;
        public OrderService(OrderDAO orderDAO,EquipmentDAO equipmentDAO,RoomDAO roomDAO) { 

            this.orderDAO = orderDAO;
            this.equipmentDAO = equipmentDAO;
            this.roomDAO = roomDAO;

           
        }

        public void CheckOrders() {
            
            List<Order> orders =orderDAO.GetAll();
            List<Order> ordersDone = new List<Order>();


            foreach (Order order in orders)
            {
                if(order.arrivalDate.Date<=DateTime.Now.Date) {
                    ordersDone.Add(order);
                    CompleteOrders(order);
                }
            }

            foreach (Order order in ordersDone)
            {
                orderDAO.Remove(order);
            }
        
        }

        public void CompleteOrders(Order order)
        {


            Room storage=roomDAO.GetRoomByNumber(order.storageRoom);
            Equipment equipment=equipmentDAO.FindByName(order.equipment);

            int currentCount;
            storage.equipmentCount.TryGetValue(equipment.name, out currentCount);
            storage.equipmentCount[equipment.name] = currentCount + order.quantity;

            roomDAO.equipmentCount.TryGetValue(equipment.name, out currentCount);
            roomDAO.equipmentCount[equipment.name] = currentCount + order.quantity;

            roomDAO.Save();


        }
    }
}
