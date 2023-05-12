using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Serializer;
using Hospital.Model;

namespace Hospital.Storage
{
    class EquipmentStorage 
    {
        private const string StoragePath = "../../../Data/equipment.json";

        private Serializer<Equipment> _serializer;


        public EquipmentStorage()
        {
            _serializer = new Serializer<Equipment>();
        }

        public List<Equipment> Load()
        {

            return _serializer.FromJSON(StoragePath);
        }

        public void Save(List<Equipment> equipment)
        {
            _serializer.ToJSON(StoragePath, equipment);
        }


    }
}
