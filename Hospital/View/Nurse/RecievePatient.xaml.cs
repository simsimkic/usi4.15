using Hospital.Controller;
using Hospital.Model;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for RecievePatient.xaml
    /// </summary>
    public partial class RecievePatient : Window, INotifyPropertyChanged
    {
        private PatientController _patientController;
        private AppointmentController _appoinmentController;

        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public string RecieveTitle { get; set; }
        public RecievePatient(  PatientController patientController,Patient patient,
                                AppointmentController appointmentController,Appointment appointment)
        {
            InitializeComponent();
            RecieveTitle = "Recieve Patient : " + patient.FirstName + " " + patient.LastName + " " + appointment.TimeSlot.Start;
            DataContext = this;
            _patientController = patientController;
            _appoinmentController = appointmentController;
            MedicalRecord = patient.MedicalRecord;
            Patient = patient;
            Appointment = appointment;
        }
        private void RecievePatient_Click(object sender, RoutedEventArgs e)
        {
            Appointment.Anamnesis = MedicalRecord.Anamnesis;
            _appoinmentController.Update();
            _patientController.Update();
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
