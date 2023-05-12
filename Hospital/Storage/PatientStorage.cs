using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Model;
using Hospital.Serializer;

namespace Hospital.Storage
{
    class PatientStorage
    {
        private const string StoragePath = "../../../Data/patients.json";

        private Serializer<Patient> _serializer;


        public PatientStorage()
        {
            _serializer = new Serializer<Patient>();
        }

        public List<Patient> Load()
        {
            return _serializer.FromJSON(StoragePath);
        }

        public void Save(List<Patient> Patients)
        {
            _serializer.ToJSON(StoragePath, Patients);
        }
    }
}
