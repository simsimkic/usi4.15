using Hospital.Model.DAO;
using Hospital.Model;
using Hospital.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller
{
    public class DoctorController
    {
        private DoctorDAO _doctors;

        public DoctorController()
        {
            _doctors = new DoctorDAO();
        }
        public Doctor GetDoctor(int doctorId)
        {
            return _doctors.GetDoctor(doctorId);
        }

        public string GetDoctorFullName(int id)
        {
            return _doctors.GetDoctorFullName(id);
        }
        public List<Doctor> GetAllDoctors()
        {
            return _doctors.GetAll();
        }

        public List<Doctor> GetSpecializedDoctors(Specialization specialization)
        {
            return _doctors.GetSpecializedDoctors(specialization);
        }

        public void Subscribe(IObserver observer)
        {
            _doctors.Subscribe(observer);
        }
    }
}
