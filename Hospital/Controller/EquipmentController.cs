using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Model.DAO;

namespace Hospital.Controller
{
    public class EquipmentController
    {
        private EquipmentDAO _equipment;

        public EquipmentController()
        {
            _equipment = new EquipmentDAO();
        }
        public List<Equipment> GetAll()
        {
            return _equipment.GetAll();
        }

        public List<Equipment> GetActiveEquipment()
        {
            return _equipment.GetActiveEquipment();
        }

        public Equipment FindByName(string name)
        {


           return _equipment.FindByName(name);
        }


        public EquipmentDAO GetDAO()
        {
            return _equipment;
        }
    }
}
