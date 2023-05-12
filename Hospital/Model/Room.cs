using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Hospital.Serializer;

namespace Hospital.Model
{
 

    public enum roomPurpose
    {
        storage,
        checkup,
        operation,
        waiting,
        patient
    };
    public class Room
    {
        public string number { get; set; }

        public roomPurpose purpose { get; set; }

        public Dictionary<string, int> equipmentCount { get; set; }

        public Room() { }
        public Room(string number, roomPurpose purpose, Dictionary<string, int> equipmentCount)
        {
            this.number = number;
            this.purpose = purpose;
            this.equipmentCount = equipmentCount;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return number;
        }

    }

}
