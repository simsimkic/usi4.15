using Hospital.Observer;
using Hospital.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model.DAO
{
    internal class DoctorDAO
    {
        private List<IObserver> _observers;

        //private DoctorStorage _doctorStorage;
        private List<Doctor> _doctors;

        public DoctorDAO()
        {
            //_doctorStorage = new PatientStorage();
            _doctors = LoadDoctors();
            _observers = new List<IObserver>();
        }

        private List<Doctor> LoadDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            Doctor d1 = new Doctor(10,"Prvi","Doctor",Specialization.Anestesiologist);
            Doctor d2 = new Doctor(5, "Drugi", "Doctor", Specialization.Dermatologist);
            Doctor d3 = new Doctor(15, "Treci", "Doctor", Specialization.Cardiologist);
            Doctor d6 = new Doctor(16, "Treci", "Doctor", Specialization.Cardiologist);
            Doctor d4 = new Doctor(20, "Cetvrti", "Doctor", Specialization.Anestesiologist);
            doctors.Add(d1);
            doctors.Add(d2);
            doctors.Add(d3);
            doctors.Add(d4);
            doctors.Add(d6);
            return doctors;
        }

        public List<Doctor> GetSpecializedDoctors(Specialization specialization)
        {
            List<Doctor> specializedDoctors = new List<Doctor>();
            foreach(Doctor doctor in _doctors)
            {
                if(doctor.Specialization == specialization)
                    specializedDoctors.Add(doctor);
            }
            return specializedDoctors;
        }

        public string GetDoctorFullName(int id) { 
            foreach(Doctor doctor in _doctors)
            {
                if(doctor.Id == id)
                {
                    return doctor.FirstName +" "+ doctor.LastName;
                }
            }
            return "";
        }
        public Doctor GetDoctor(int id)
        {
            foreach (Doctor doctor in _doctors)
            {
                if (doctor.Id == id)
                {
                    return doctor;
                }
            }
            return null;
        }
        public List<Doctor> GetAll()
        {
            return _doctors;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
