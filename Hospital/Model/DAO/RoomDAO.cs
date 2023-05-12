using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Hospital.Model;
using Hospital.Storage;
using Hospital.Controller;
using System.Configuration;

namespace Hospital.Model.DAO
{
    public class RoomDAO
    {
        public Dictionary<string, int> equipmentCount;
        public List<Room> rooms = new List<Room>();
        private RoomStorage _storage;

        public RoomDAO()
        {
            _storage = new RoomStorage();
            rooms = _storage.Load();
            
            SetEquipmentCount();

        }

        public List<Room> GetAll() => rooms;

        public List<Room> GetStorageRooms() { 
            
            List<Room> storageRooms = new List<Room>();

            foreach(Room room in rooms)
            {
                if (room.purpose == roomPurpose.storage)
                {
                    storageRooms.Add(room);
                }
            }

            return storageRooms;
        }

        public Room GetRoomByNumber(string s)
        {
            foreach (Room room in rooms)
            {
                if (room.number == s)
                {
                    return room;
                }
            }
            return null;

        }

        public List<string> GetStorageEquipment()
        {

            List<string> storageEquipment = new List<string>();

            foreach (Room room in rooms)
            {

                if (room.purpose == roomPurpose.storage)
                {

                    foreach (KeyValuePair<string, int> entry in room.equipmentCount)
                    {

                        storageEquipment.Add(entry.Key);

                    }
                }

            }

            return storageEquipment;
        }

        public void SetEquipmentCount()
        {
            equipmentCount = new Dictionary<string, int>();
            foreach (Room room in rooms)
            {
                foreach (KeyValuePair<string, int> kv in room.equipmentCount)
                {
                    int count = kv.Value;
                    string equipmentName = kv.Key;


                    int currentCount;

                    equipmentCount.TryGetValue(equipmentName, out currentCount);

                    equipmentCount[equipmentName] = currentCount + count;
                }
            }
        }

        public List<Room> GetRoomsWithEquipment(string name)
        {
            HashSet<Room> roomsWithEquipment = new HashSet<Room>();

            foreach (Room room in rooms) { 

                foreach(KeyValuePair<string,int> entry in room.equipmentCount)
                {
                    int currentCount;

                    room.equipmentCount.TryGetValue(name, out currentCount);

                    if (currentCount!=0)
                    {
                        roomsWithEquipment.Add(room);
                    }
                }
            }

            return roomsWithEquipment.ToList();
        }

        public void Save()
        {
            _storage.Save(rooms);
        }





    }

 

}
