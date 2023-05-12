using System;
using System.Collections.Generic;
using Hospital.Model;
using Hospital.Model.DAO;
using Hospital.Observer;
using Hospital.View.PatientView;

namespace Hospital.Controller
{
    /// <summary>
    /// Controller is responsible for connecting view and model.
    /// Controller is used by view to retrieve information
    /// from the model and to send events.
    /// </summary>
    public class AppointmentController
    {
        private AppointmentDAO _appointments;

        public AppointmentController()
        {
            _appointments = new AppointmentDAO();
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointments.GetAll();
        }

        public Appointment GetPatientReception(int id) {
            return _appointments.GetPatientReception(id);
        }

        public bool IsAvailable(int doctorId, TimeSlot requestedTimeSlot)
        {
            if (_appointments.IsAvailable(doctorId, requestedTimeSlot))
                return true;
            return false;
        }
        public List<Appointment> FindSuggestedAppointment(List<Doctor> doctors)
        {
            return _appointments.FindSuggestedAppointments(doctors);
        }
        public void Create(Appointment appointment)
        {
            _appointments.Add(appointment);
        }
        public TimeSlot FindFreeTimeSlot(int doctorId,int duration, int nextHours)
        {
            return _appointments.FindFreeTimeSlot(doctorId, duration, nextHours);
        }

        public List<Appointment> GetMovableAppointments(int doctorId, int duration)
        {
            return _appointments.GetMovableAppointments(doctorId, duration);
        }
        public List<Appointment> GetPatientAppointments(int patientId)
        {
            return _appointments.GetPatientAppointments(patientId);
        }
        public List<Appointment> GetPastAppointments(int patientId)
        {
            return _appointments.GetPastAppointments(patientId);
        }
        public TimeSlot FindFreeTimeSlot(int doctorId, DateTime before)
        {
            return _appointments.FindFreeTimeSlot(doctorId, before);
        }
        public TimeSlot FindFreeTimeSlot(int doctorId, TimeSpan from, TimeSpan to, DateTime before)
        {
            return _appointments.FindFreeTimeSlot(doctorId, from, to, before);
        }

        public void Update()
        {
            _appointments.Update();

        }
        public void Delete(Appointment appointment)
        {
            _appointments.Remove(appointment);
        }

        public void Subscribe(IObserver observer)
        {
            _appointments.Subscribe(observer);
        }


    }
}