using Hospital.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model.DAO
{
    public class ReorganizationDAO
    {
        public List<Reorganization> reorganizations;

        private ReorganizationStorage _storage;

        public ReorganizationDAO()
        {
            _storage = new ReorganizationStorage();
            reorganizations = _storage.Load();

        }

        public List<Reorganization> GetAll()
        {
            return reorganizations;
        }

        public void Add(Reorganization reorganization)
        {
            reorganizations.Add(reorganization);
            _storage.Save(reorganizations);

        }



        public void Remove(Reorganization reorganization)
        {
            reorganizations.Remove(reorganization);
            _storage.Save(reorganizations);
        }

    }
}
