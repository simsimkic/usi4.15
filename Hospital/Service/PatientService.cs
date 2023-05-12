using Hospital.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Model;
using Hospital.Model.DAO;

namespace Hospital.Service
{

    internal class PatientService
    {
        public string input;
        public PatientService(string input) { 
            this.input = input;
        }

        public List<Patient> SearchPatients(List<Patient> patients)
        {
            List<Patient> searchResult = new List<Patient>();

            foreach (Patient patient in patients)
            {

                if (Convert.ToString(patient.Id).Contains(input))
                {

                    searchResult.Add(patient);
                }
            }

            return searchResult;
        }
    }
}
