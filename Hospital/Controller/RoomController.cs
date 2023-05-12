using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Model.DAO;
using Hospital.Model;
using Hospital.View;

namespace Hospital.Controller
{
   public class RoomController
    {
        private RoomDAO _rooms;

        public RoomController()
        {
            _rooms = new RoomDAO();
        }
        public List<Room> GetAll()
        {
            return _rooms.GetAll();
        }

        public void Update()
        {
            _rooms.Save();
        }

        public List<Room> GetStorageRooms()
        {

           return _rooms.GetStorageRooms();
        }

        public int GetEquipmentCount(Equipment equipment)
        {
            int currentCount;

            _rooms.equipmentCount.TryGetValue(equipment.name, out currentCount);
            return currentCount;
        }

        public List<Room> GetRoomsWithEquipment(string name)
        {
            return _rooms.GetRoomsWithEquipment(name);
        }

        public RoomDAO GetDAO()
        {
            return _rooms;
        }
    }
}
