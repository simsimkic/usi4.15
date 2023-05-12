using Hospital.Model;
using Hospital.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Storage
{
    public class ReorganizationStorage
    {

        private const string StoragePath = "../../../Data/reorganizations.json";

        private Serializer<Reorganization> _serializer;


        public ReorganizationStorage()
        {
            _serializer = new Serializer<Reorganization>();
        }

        public List<Reorganization> Load()
        {

            return _serializer.FromJSON(StoragePath);
        }

        public void Save(List<Reorganization> reorganization)
        {
            _serializer.ToJSON(StoragePath, reorganization);
        }
    }
}
