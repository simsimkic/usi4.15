using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Observer;
using Hospital.Storage;
using System.Diagnostics;
using Hospital.Model;
namespace Hospital.Model.DAO
{
    class PatientDAO : ISubject
    {
        private List<IObserver> _observers;

        private PatientStorage _patianteStorage;
        private List<Patient> _patients;

        public PatientDAO()
        {
            _patianteStorage = new PatientStorage();
            _patients = _patianteStorage.Load();
            _observers = new List<IObserver>();
        }

        public Patient GetPatient(int id)
        {
            foreach (Patient patient in _patients)
            {
                if (patient.Id == id)
                    return patient;
            }
            return null;
        }

        public int GetPatientNextId()
        {
            if(_patients.Count > 0) 
                return _patients.Max(s => s.Id) + 1;
            return 0;
        }

        public void Add(Patient patient)
        {
            patient.Id = GetPatientNextId();
            _patients.Add(patient);
            _patianteStorage.Save(_patients);
            NotifyObservers();
        }

        public void Remove(Patient patient)
        {
            _patients.Remove(patient);
            _patianteStorage.Save(_patients);
            NotifyObservers();
        }

        public void Update()
        {
            _patianteStorage.Save(_patients);
            NotifyObservers();
        }

        public List<Patient> GetAll()
        {
            return _patients;
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
