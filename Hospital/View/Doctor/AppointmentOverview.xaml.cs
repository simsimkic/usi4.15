using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using Hospital.Observer;



namespace Hospital.View
{

    /// <summary>
    /// Interaction logic for AppointmentOverview.xaml
    /// </summary>
    public partial class AppointmentOverview : Window, IObserver
    {
        private AppointmentController _appointmentController;

        private PatientController _patientController;

        public ObservableCollection<Appointment> Appointments { get; set; }
        public Appointment SelectedAppointment { get; set; }

        public Doctor ActiveDoctor = new Doctor(10, "Ivana", "Savkovic", Specialization.Neonatologist);

        public AppointmentOverview(DateTime time)
        {
            InitializeComponent();
            DataContext = this;

            _appointmentController = new AppointmentController();
            _patientController = new PatientController();

            _appointmentController.Subscribe(this);

            Appointments = new ObservableCollection<Appointment>(_appointmentController.GetAllAppointments());
            ShowTodaysAppointments(time);

        }

        private void ShowTodaysAppointments(DateTime today)
        {
            DateTime endDate = today.AddDays(3);

            var todaysAppointments = _appointmentController.GetAllAppointments()
                .Where(a => a.TimeSlot.Start >= today && a.TimeSlot.Start <= endDate)
                .ToList();

            Appointments = new ObservableCollection<Appointment>(todaysAppointments);
        }

        private void ShowCreateAppointmentWindow_Click(object sender, RoutedEventArgs e)
        {
            CreateAppointment createAppointment = new CreateAppointment(_appointmentController, ActiveDoctor.Id);
            createAppointment.ShowDialog();
        }
        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                MessageBoxResult result = ConfirmAppointmentDeletion();

                if (result == MessageBoxResult.Yes)
                    _appointmentController.Delete(SelectedAppointment);
            }
            else
            {
                MessageBox.Show("Odaberite termin koji želite da izbrišete.");
            }
        }

        private void SearchPatients_Click(object sender, RoutedEventArgs e)
        {
            SearchPatients sp = new SearchPatients();
            sp.ShowDialog();
        }

        private MessageBoxResult ConfirmAppointmentDeletion()
        {
            string sMessageBoxText = $"Da li ste sigurni da želite da izbrišete termin\n{SelectedAppointment}";
            string sCaption = "Porvrda brisanja";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }

        private void UpdateAppointmentList()
        {
            Appointments.Clear();
            foreach (var appointment in _appointmentController.GetAllAppointments())
            {
                Appointments.Add(appointment);
            }
        }

        public void Update()
        {
            UpdateAppointmentList();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void ShowMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                Patient viewPatient = _patientController.GetPatient(SelectedAppointment.IdPatient);
                MedicalRecordView medicalRecordView = new MedicalRecordView(viewPatient);
                medicalRecordView.Show();
            }

        }

        private void UpdateAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                UpdateAppointmentDoctor updateAppointmentDoctor = new UpdateAppointmentDoctor(_appointmentController, SelectedAppointment);
                updateAppointmentDoctor.Show();
            }
            else
            {
                MessageBox.Show("Pick appointment to update.");
            }
        }

        private void StartMedicalCheckUp_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                Patient checkUpPatient = _patientController.GetPatient(SelectedAppointment.IdPatient);
                if(checkUpPatient != null) 
                { 
                    MedicalCheckUp medicalCheckUp = new MedicalCheckUp(checkUpPatient, SelectedAppointment, _appointmentController, _patientController);
                    medicalCheckUp.Show();
                }
                else
                {
                    MessageBox.Show("Patient not found");
                }
            }
        }
    }
}
