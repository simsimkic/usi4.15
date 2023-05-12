using Hospital.Controller;
using Hospital.Model;
using Hospital.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for MedicalCheckUp.xaml
    /// </summary>
    public partial class MedicalCheckUp : Window, INotifyPropertyChanged, IObserver
    {
        private PatientController _patientController;

        private AppointmentController _appointmentController;

        public ObservableCollection<Patient> Patients { get; set; }

        public Patient SelectedPatient { get; set; }

        public Appointment SelectedAppointment { get; set; }

        public Doctor ActiveDoctor = new Doctor(111, "Ivana", "Savkovic", Specialization.Neonatologist);

        //TextBox tb;
        public MedicalCheckUp(Patient patient, Appointment selectedAppointment, AppointmentController appointmentController, PatientController patientController)
        {
            InitializeComponent();
            DataContext = this;
            

            _patientController = patientController;
            _appointmentController = appointmentController;

            _patientController.Subscribe(this);
            _appointmentController.Subscribe(this);

            SelectedPatient = patient;
            SelectedAppointment = selectedAppointment;
            
            Patients = new ObservableCollection<Patient>(_patientController.GetAllPatients());



        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecordView medicalRecordView = new MedicalRecordView(SelectedPatient);
            medicalRecordView.Show();

        }

        private void ShowEditMedicalRecordWindow_Click(object sender, RoutedEventArgs e)
        {
            EditMedicalRecord updatePatient = new EditMedicalRecord(_patientController, SelectedPatient);
            updatePatient.Show();
                
        }

        private void UpdateAnamnesis_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient.IsValid)
            {
                SelectedAppointment.Anamnesis = SelectedPatient.MedicalRecord.Anamnesis;
                _patientController.Update();
                _appointmentController.Update();
                DynamicEquipmentAfterMeducalCheckUp deamcu = new DynamicEquipmentAfterMeducalCheckUp();
                deamcu.Show();
                Close();
            }
        }

        private void UpdatePatientList()
        {
            Patients.Clear();
            foreach (var appointment in _patientController.GetAllPatients())
            {
                Patients.Add(appointment);
            }
        }

        public void Update()
        {
            UpdatePatientList();
        }

    }
}
