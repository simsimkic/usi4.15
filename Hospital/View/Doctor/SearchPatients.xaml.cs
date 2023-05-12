using Hospital.Controller;
using Hospital.Model;
using Hospital.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for SearchPatients.xaml
    /// </summary>
    public partial class SearchPatients : Window, IObserver
    {

        private PatientController _controller;

        List<string> options = new List<string>();

        public ObservableCollection<Patient> Patients { get; set; }

        public Patient SelectedPatient { get; set; }

        public Doctor ActiveDoctor = new Doctor(10, "Ivana", "Savkovic", Specialization.Neonatologist);

        public SearchPatients()
        {
            InitializeComponent();
            DataContext = this;

            _controller = new PatientController();
            _controller.Subscribe(this);

            options.Add("All patients");
            options.Add("My patients");
            this.typeCb.ItemsSource = options;
            this.typeCb.SelectedIndex = 0;
            this.typeCb.SelectionChanged += new SelectionChangedEventHandler(ShowPatients);
            //this.typeCB.SelectedIndexChanged += typeCB_SelectedIndexChanged;

            Patients = new ObservableCollection<Patient>(_controller.GetAllPatients());
            //ShowDoctorsPatients();
        }

        private void SearchPatientById(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(patientIdTxt.Text))
            {
                int patientId = int.Parse(patientIdTxt.Text);
                Patients = new ObservableCollection<Patient>(_controller.GetAllPatients()
                    .Where(p => p.Id == patientId)
                    .ToList());
                DataGrid dg = (DataGrid)FindName("myDataGrid");
                dg.ItemsSource = Patients;
            }
        }

        private void ShowPatients(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)FindName("myDataGrid");
            ComboBox typeCB = (ComboBox)sender;

            if (typeCB.SelectedItem == null)
                return;

            if (typeCB.SelectedItem.ToString() == "My patients")
            {
                List<int> patientsIds = GetPatientExaminedByDoctor();

                Patients = new ObservableCollection<Patient>(_controller.GetAllPatients()
                    .Where(p => patientsIds.Contains(p.Id))
                    .ToList());
            }
            else if (typeCB.SelectedItem.ToString() == "All patients")
            {
                Patients = new ObservableCollection<Patient>(_controller.GetAllPatients().ToList());
            }

            dg.ItemsSource = Patients;
        }

        private void UpdatePatientList()
        {
            Patients.Clear();
            foreach (var appointment in _controller.GetAllPatients())
            {
                Patients.Add(appointment);
            }
        }

        public void Update()
        {
            UpdatePatientList();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void ShowMedicalRecord_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedPatient != null)
            {

                List<int> patientsIds = GetPatientExaminedByDoctor();

                if (patientsIds.Contains(SelectedPatient.Id))
                {
                    MedicalRecordView medicalRecordView = new MedicalRecordView(SelectedPatient);
                    medicalRecordView.Show();
                }
                else
                {
                    MessageBox.Show("You do not have access to the medical records of this patient.");

                }

            }
            else
            {
                MessageBox.Show("Please select a patient.");
            }

        }

        private void ShowEditMedicalRecordWindow_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                List<int> patientsIds = GetPatientExaminedByDoctor();

                if (patientsIds.Contains(SelectedPatient.Id))
                {
                    EditMedicalRecord updatePatient = new EditMedicalRecord(_controller, SelectedPatient);
                    updatePatient.Show();
                }
                else
                {
                    MessageBox.Show("You do not have access to the medical records of this patient.");

                }
            }
            else
            {

                MessageBox.Show("Pick patient to update.");
            }
        }

        private List<int> GetPatientExaminedByDoctor()
        {
            AppointmentController ac = new AppointmentController();
            List<int> patientsIds = ac.GetAllAppointments()
                .Where(a => a.IdDoctor == ActiveDoctor.Id)
                .Select(a => a.IdPatient)
                .ToList();
            return patientsIds;
        }

    }
}
