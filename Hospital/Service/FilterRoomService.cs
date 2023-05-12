using Hospital.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model.Service
{
    public class FilterRoomService
    {

        RoomDAO roomDAO;

        public FilterRoomService(RoomDAO roomDAO)
        {

            this.roomDAO = roomDAO;


        }

        public List<Room> FilterRoomsByEquipment(string eqName,List<Room> roomsToFilter)
        {
            List<Room> roomsWithEquipment=roomDAO.GetRoomsWithEquipment(eqName);

            return roomsToFilter.Intersect(roomsWithEquipment).ToList<Room>();
        }


    }
}
