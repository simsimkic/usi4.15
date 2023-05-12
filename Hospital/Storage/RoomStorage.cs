using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Serializer;
using Hospital.Model;

namespace Hospital.Storage
{
    class RoomStorage
    {

            private const string StoragePath = "../../../Data/rooms.json";

            private Serializer<Room> _serializer;

        
            public RoomStorage()
            {
                _serializer = new Serializer<Room>();
            }

            public List<Room> Load()
            {

                return _serializer.FromJSON(StoragePath);
            }

            public void Save(List<Room> rooms)
            {
                _serializer.ToJSON(StoragePath, rooms);
            }



    }
}
