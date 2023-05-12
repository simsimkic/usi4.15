using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Model.DAO;

namespace Hospital.Model
{
    public class SearchService
    {
       public string input;

        EquipmentDAO equipmentDAO;
        public SearchService(string input, EquipmentDAO equipmentDAO)
        {
            this.input = input;
            this.equipmentDAO = equipmentDAO;
        }

        public List<Equipment> SearchEquipment()
        {

            List<Equipment> searchResult = new List<Equipment>();

            foreach (Equipment equipment in equipmentDAO.equipment)
            {           

                if (equipment.name.Contains(input))
                {

                    searchResult.Add(equipment);
                }
            }

            return searchResult;

        }


    }

}
