using Hospital.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Model.Service
{
    internal class ReorganizationService
    {
        ReorganizationDAO reorganizationDAO;
        EquipmentDAO equipmentDAO;
        RoomDAO roomDAO;
        public ReorganizationService(ReorganizationDAO reorganizatioDAO, EquipmentDAO equipmentDAO,RoomDAO roomDAO) { 

            this.reorganizationDAO = reorganizatioDAO;
            this.equipmentDAO = equipmentDAO;
            this.roomDAO = roomDAO;

           
        }

        public void CheckReorganizations() {
            
            List<Reorganization> reorganizations = reorganizationDAO.GetAll();
            List<Reorganization> reorganizationsDone = new List<Reorganization>();


            foreach (Reorganization reorganization in reorganizations)
            {
                if(reorganization.dueDate.Date<=DateTime.Now.Date) {
                    reorganizationsDone.Add(reorganization);
                    CompleteReorganization(reorganization);
                }
            }

            foreach (Reorganization reorganization in reorganizationsDone)
            {
                reorganizationDAO.Remove(reorganization);
            }
        
        }

        public void CompleteReorganization(Reorganization reorganization)
        {


            Room from=roomDAO.GetRoomByNumber(reorganization.from);
            Room to = roomDAO.GetRoomByNumber(reorganization.to);
            Equipment equipment=equipmentDAO.FindByName(reorganization.equipment);

            if(from.equipmentCount[equipment.name] - reorganization.quantity >= 0)
            {
                from.equipmentCount[equipment.name] -= reorganization.quantity;
            }
            else
            {
                MessageBox.Show("Reogranization:"+reorganization.ToString()+"was not completed due to insufficient  equipment");
                return;
            }


            int currentCount;
            to.equipmentCount.TryGetValue(equipment.name, out currentCount);
            to.equipmentCount[equipment.name] = currentCount + reorganization.quantity;

            roomDAO.equipmentCount[equipment.name] += reorganization.quantity;

            roomDAO.Save();
           

        }
    }
    

}
