using Hospital.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hospital.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Numerics;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for CreateEmergencyAppointment.xaml
    /// </summary>
    public partial class CreateEmergencyAppointment : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Appointment> ShowMovableAppointments { get; set; }

        private AppointmentController _appointmentController;
        private DoctorController _doctorController;
        public Patient SelectedPatient;
        public ObservableCollection<int> Hours { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public int Postponment { get; set; }
        public int Duration { get; set; }
        public bool IsOperation { get; set; }
        public Specialization SelectedSpecialization { get; set; }
        public CreateEmergencyAppointment(Patient selectedPatient,
                                          AppointmentController appointmentController)
        {
            InitializeComponent();
            DataContext = this;
            _appointmentController = appointmentController;
            _doctorController = new DoctorController();
            SelectedPatient = selectedPatient;
            ShowMovableAppointments = new ObservableCollection<Appointment>(new List<Appointment>());
            Hours = new ObservableCollection<int>
            {
                4,
                5,
                6,
                7,
                8,
                24,
                72
            };
            Postponment = 4;
            SelectedSpecialization = Specialization.Cardiologist;
        }

        

        
        private void CreateEmergancyAppointment_Click(object sender, RoutedEventArgs e)
        {
            List<Doctor> specializedDoctors = _doctorController.GetSpecializedDoctors(SelectedSpecialization);
            if(specializedDoctors.Count == 0)
            {
                MessageBox.Show("There are no available doctors");
                return;
            }

            Duration = 35;
            if (!IsOperation)
            {
                Duration = 15;
            }

            Appointment emergencyAppointment = GetEmergancyAppointment(specializedDoctors);

            if(emergencyAppointment != null)
            {
                MessageBox.Show("Emergency Appointment Created: \n " +
                    "Date and Time: " + emergencyAppointment.TimeSlot.Start + "\n"+
                    "Doctor: " + _doctorController.GetDoctorFullName(emergencyAppointment.IdDoctor));
                Close();
                return;
            }
            if (emergencyAppointment == null)
            {
                List<Appointment> MovableAppointments = GetMovableAppointments(specializedDoctors);
                List<Appointment> sortedMovableAppointments = SortMovableAppointments(MovableAppointments);
                UpdateAppointmentsDataGrid(sortedMovableAppointments);
            }
        }

        private List<Appointment> GetMovableAppointments(List<Doctor> specializedDoctors)
        {
            List<Appointment> MovableAppointments = new List<Appointment>(); 
            foreach (Doctor doctor in specializedDoctors)
            {
                List<Appointment> doctorMovableAppointments = _appointmentController.GetMovableAppointments(doctor.Id, Duration);
                MovableAppointments.AddRange(doctorMovableAppointments);
            }
            return MovableAppointments;
        }
        private List<Appointment> SortMovableAppointments(List<Appointment> MovableAppointments)
        {
            Dictionary<Appointment, TimeSlot> sortedMovableAppointments = new Dictionary<Appointment, TimeSlot>();
            foreach (Appointment MovableAppointment in MovableAppointments)
            {
                sortedMovableAppointments[MovableAppointment] =
                    _appointmentController.FindFreeTimeSlot(MovableAppointment.IdDoctor,
                    MovableAppointment.TimeSlot.Duration,
                    Postponment);
            }
            sortedMovableAppointments = sortedMovableAppointments.OrderBy(appointment => appointment.Value).ToDictionary(appointment => appointment.Key, appointment => appointment.Value);
            return new List<Appointment>(sortedMovableAppointments.Keys);

        }

        private void UpdateAppointmentsDataGrid(List<Appointment> appointments)
        {
            ShowMovableAppointments.Clear();
            int i = 0;
            foreach (var appointment in appointments)
            {
                ShowMovableAppointments.Add(appointment);
                i++;
                if (i == 5)
                    break;
            }
        }
        private Appointment GetEmergancyAppointment(List<Doctor> specializedDoctors)
        {

            foreach (Doctor doctor in specializedDoctors)
            {
                TimeSlot emergencyTimeSlot = _appointmentController.FindFreeTimeSlot(
                    doctor.Id,
                    Duration, 
                    2);
                if (emergencyTimeSlot != null)
                {
                        Appointment emergencyAppointment = new Appointment(
                            doctor.Id,
                            SelectedPatient.Id,
                            IsOperation,
                            emergencyTimeSlot.Start,
                            "",
                            doctor.Specialization);
                    _appointmentController.Create(emergencyAppointment);
                    return emergencyAppointment;
                }
            }
            return null;
        }

        private void MoveAppointment_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedAppointment == null)
            {
                return;
            }
            Doctor usedDoctor = _doctorController.GetDoctor(SelectedAppointment.IdDoctor);
            List<Doctor> specializedDoctors = _doctorController.GetSpecializedDoctors(usedDoctor.Specialization);
            foreach (Doctor doctor in specializedDoctors)
            {
                TimeSlot nextFreeTimeSlot = _appointmentController.FindFreeTimeSlot(doctor.Id, SelectedAppointment.TimeSlot.Duration, Postponment);
                if (nextFreeTimeSlot != null)
                {
                    CreateNewAppointment(nextFreeTimeSlot);
                    UpdateSelectedAppointment();
                    MessageBox.Show("Succsesfully moved appointment to: \n" + nextFreeTimeSlot.Start);
                    Close();
                    return;
                }
            }
            MessageBox.Show("Unable to move appointment please choose another appointment or increase time for moving");
        }

        private void UpdateSelectedAppointment()
        {
            SelectedAppointment.IdPatient = SelectedPatient.Id;
            SelectedAppointment.TimeSlot.Duration = Duration;
            SelectedAppointment.Anamnesis = "";
        }
        private void CreateNewAppointment(TimeSlot nextFreeTimeSlot)
        {
            Appointment newAppointment = new Appointment(
                SelectedAppointment.IdDoctor,
                SelectedAppointment.IdPatient,
                SelectedAppointment.IsOperation,
                nextFreeTimeSlot.Start,
                "",
                SelectedAppointment.Specialization
                );
            _appointmentController.Create(newAppointment);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
