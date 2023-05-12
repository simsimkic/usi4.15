using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Model.DAO;
using Hospital.Model.Service;

namespace Hospital.Model
{
    public class FilterEquipmentService
    {

        RoomDAO roomDAO;
        EquipmentDAO equipmentDAO;

        public FilterEquipmentService( RoomDAO roomDAO, EquipmentDAO equipmentDAO)
        {

            this.roomDAO = roomDAO;
            this.equipmentDAO = equipmentDAO;

        }



        public List<Equipment> FilterStorage(bool storage,List<Equipment> equipmentToFilter)
        {

            if (!storage)
            {

                List<Equipment> equipmentsInStorage = new List<Equipment>();

                List<string> equipmentNames = roomDAO.GetStorageEquipment();

                foreach (string equipmentName in equipmentNames)
                {
                    equipmentsInStorage.Add(equipmentDAO.FindByName(equipmentName));
                }

                equipmentToFilter = equipmentToFilter.Except(equipmentsInStorage).ToList<Equipment>();

            }

            return equipmentToFilter;
        }

        public List<Equipment> FilterEquipmentType(int eqType,List<Equipment> equipmentToFilter)
        {   

            if(eqType==-1) {
                return equipmentToFilter;
            }

            List<Equipment> filteredType = new List<Equipment>();

            foreach (Equipment equipment in equipmentDAO.equipment)
            {

                if (equipment.type == (equipmentType)eqType)
                {

                    filteredType.Add(equipment);
                }
            }

            return equipmentToFilter.Intersect(filteredType).ToList<Equipment>();
           
        }


        public List<Equipment> FilterRoomPurpose(int roomPurpose, List<Equipment> equipmentToFilter)
        {
            if (roomPurpose == -1)
            {
                return equipmentToFilter;
            }

            List<Equipment> filteredPurpose = new List<Equipment>();

            foreach (Room room in roomDAO.rooms)
            {

                if (room.purpose == (roomPurpose)roomPurpose)
                {

                    foreach (KeyValuePair<string, int> entry in room.equipmentCount)
                    {
                        Equipment equipment = equipmentDAO.FindByName(entry.Key);

                        filteredPurpose.Add(equipment);

                    }
                }

            }
            return equipmentToFilter.Intersect(filteredPurpose).ToList<Equipment>();
           

        }

        public List<Equipment> FilterEquipmentQuantity(int quantityOption, List<Equipment> equipmentToFilter)
        {

            if(quantityOption==-1)
            {
                return equipmentToFilter;
            }

            List<Equipment> filteredQuantity = new List<Equipment>();

            foreach (Equipment equipment in equipmentToFilter)
            {


                int count;

                roomDAO.equipmentCount.TryGetValue(equipment.name  , out count);

                switch (quantityOption)
                {
                    case 0:
                        if (count == 0)
                        {

                            filteredQuantity.Add(equipment);

                        }
                        break;
                    case 1:
                        if (count > 0 && count < 10)
                        {

                            filteredQuantity.Add(equipment);

                        }
                        break;
                    case 2:

                        if ( count > 10)
                        {

                            filteredQuantity.Add(equipment);

                        }

                        break;

                    default:

                        break;
                }
            }
            return equipmentToFilter.Intersect(filteredQuantity).ToList<Equipment>();
            
        }


    }

}
