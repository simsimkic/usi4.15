using Hospital.Model;
using Hospital.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public enum equipmentType
    {
        passive,
        active
    };

    public enum equipmentPurpose
    {
        checkup,
        operation,
        room,
        corridor
    };



    public class Equipment
    {
        public string name { get; set; }
        public equipmentType type { get; set; }
        public equipmentPurpose purpose { get; set; }

        public Equipment() { }
        public Equipment(string name, equipmentType type, equipmentPurpose purpose)
        {
            this.name = name;
            this.type = type;
            this.purpose = purpose;
        }

       

        public override bool Equals(object obj)
        {
            return Equals(obj as Equipment);
        }

        public bool Equals(Equipment other)
        {
            return other != null &&
                   name == other.name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        }

        public override string ToString()
        {
            return name;
        }

    }

}
