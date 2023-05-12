using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
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
using Hospital.Controller;
using Hospital.Model;
using Hospital.Storage;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for CreateAppointment.xaml
    /// </summary>
    public partial class CreateAppointment : Window, INotifyPropertyChanged
    {
        private AppointmentController _controller;
        public Appointment Appointments { get; set; }
        public CreateAppointment(AppointmentController controller, int idDoctor)
        {
            InitializeComponent();
            DataContext = this;
            Appointments = new Appointment();
            Appointments.IdDoctor = idDoctor;
            Appointments.TimeSlot.Start = DateTime.Now;
            _controller = controller;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (Appointments.IsValid == "")
            {
                
                if (CheckUpCheckBox.IsChecked == true) //Salje se kao promenljiva 
                    Appointments.TimeSlot.Duration = 35;
                else
                    Appointments.TimeSlot.Duration = 15;
                

                if (IsDoctorAvailable())
                {
                    if (PatientExist() & isPatientAvailable())
                    {
                        _controller.Create(Appointments);
                        
                    }
                    Close();
                }
                //else if ()
                else
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Create appointment"+ Appointments.IsValid);
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private bool IsDoctorAvailable()
        {

            List<Appointment> appointmentsForCheck = _controller.GetAllAppointments();
            bool isDoctorAvailable = true;
            foreach (Appointment item in appointmentsForCheck)
            {
                DateTime busyStart = item.TimeSlot.Start;
                int howLong = item.TimeSlot.Duration;
                DateTime busyEnd = busyStart.AddMinutes(howLong);

                if (Appointments.TimeSlot.Start >= busyStart && Appointments.TimeSlot.Start <= busyEnd)
                {
                    MessageBox.Show("Doctor is not available.");
                    isDoctorAvailable = false;
                    break;
                }

            }

            return isDoctorAvailable;
        }

        private bool PatientExist()
        {
            bool isPatientExist = false;
            PatientController pc = new PatientController();
            List <Patient> list = pc.GetAllPatients();

            foreach (Patient item in list)
            {
                if (item.Id == Appointments.IdPatient) 
                {
                    isPatientExist = true;
                    break;
                }
            }
            if (!isPatientExist)
            {
                MessageBox.Show("Patient doesn`t exist.");
            }
            
            return isPatientExist;
        }

        private bool isPatientAvailable()
        {
            bool isAvailable = true;
            List<Appointment> list = _controller.GetAllAppointments();
            List<DateTime> patientTime = new List<DateTime>();

            foreach (Appointment item in list)
            {
                if (item.IdPatient == Appointments.IdPatient)
                {
                    patientTime.Add(item.TimeSlot.Start);
                }
            }

            foreach(DateTime item in patientTime)
            {
                if (item.Equals(Appointments.TimeSlot.Start))
                {
                    MessageBox.Show("Patient is not available.");
                    isAvailable = false;
                }
            }
            return isAvailable;
        }
    }
}
