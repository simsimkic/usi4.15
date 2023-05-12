
using System.Collections.ObjectModel;
using System.Windows;
using Hospital.Model;
using Hospital.Controller;
using Hospital.Observer;
using Hospital.View;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientOverview.xaml
    /// </summary>
    public partial class PatientOverview : Window, IObserver
    {
        private PatientController _patientController;
        private AppointmentController _appointmentController;
        public ObservableCollection<Patient> Patients { get; set; }
        public Patient SelectedPatient { get; set; }

        public PatientOverview()
        {
            InitializeComponent();
            DataContext = this;

            _patientController = new PatientController();
            _patientController.Subscribe(this);
            _appointmentController = new AppointmentController();

            Patients = new ObservableCollection<Patient>(_patientController.GetAllPatients());
        }

        private void ShowCreatePatientWindow_Click(object sender, RoutedEventArgs e)
        {
            CreatePatient createPatient = new CreatePatient(_patientController);
            createPatient.Show();
        }
        private void ShowUpdatePatientWindow_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedPatient != null)
            {
                UpdatePatient updatePatient = new UpdatePatient(_patientController, SelectedPatient);
                updatePatient.Show();
            }
            else
            {

                MessageBox.Show("Pick patient to update.");
            }
        }
        private void ShowRecievePatientWindow_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient == null) 
            { 
                MessageBox.Show("Pick patient to recieve.");
                return;
            }
            Appointment patientReception = _appointmentController.GetPatientReception(SelectedPatient.Id);
            if (patientReception != null)
            {
                RecievePatient recievePatient = new RecievePatient(_patientController, SelectedPatient,_appointmentController, patientReception);
                recievePatient.Show();

            }
            else
            {
                MessageBox.Show("Patient doesn't have any appointments to be recieved to.");
                return;
            }
        }
        private void ShowEmergencyAppointmentWindow_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("Pick patient for appointment.");
                return;
            }
            CreateEmergencyAppointment createEmergencyAppointment = new CreateEmergencyAppointment(SelectedPatient, _appointmentController);
            createEmergencyAppointment.Show();
        }
        private void ShowMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                MedicalRecordView medicalRecordView = new MedicalRecordView(SelectedPatient);
                medicalRecordView.Show();
            }

        }
        private void DeletePatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                MessageBoxResult result = ConfirmPatientDeletion();

                if (result == MessageBoxResult.Yes)
                    _patientController.Delete(SelectedPatient);
            }
            else
            {

                MessageBox.Show("Pick patient to delete.");
            }
        }

        private MessageBoxResult ConfirmPatientDeletion()
        {
            string sMessageBoxText = $"Are you sure you want to delete Patient: \n{SelectedPatient}";
            string sCaption = "Confirm delete";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }

        private void UpdatePatientList()
        {
            Patients.Clear();
            foreach (var patient in _patientController.GetAllPatients())
            {
                Patients.Add(patient);
            }
        }

        public void Update()
        {
            UpdatePatientList();
        }
    }
}
