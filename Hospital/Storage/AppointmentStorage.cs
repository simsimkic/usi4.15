using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Model;
using Hospital.Serializer;

namespace Hospital.Storage
{
    class AppointmentStorage
    {

        private const string StoragePath = "../../../Data/appointments.json";
        private Serializer<Appointment> _serializer;

        public AppointmentStorage()
        {
            _serializer = new Serializer<Appointment>();
        }

        public List<Appointment> Load()
        {
            return _serializer.FromJSON(StoragePath);
        }
        
        public void Save(List<Appointment> appointment)
        {
            _serializer.ToJSON(StoragePath, appointment);
        }
    }
}
