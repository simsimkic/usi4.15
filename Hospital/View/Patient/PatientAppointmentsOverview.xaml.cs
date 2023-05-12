using System.Collections.ObjectModel;
using System.Windows;
using Hospital.Controller;
using Hospital.Model;
using Hospital.Observer;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for StudentsOverview.xaml
    /// This is the view part of this application.
    /// It's responsible for previewing data to the user and
    /// for retrieving data from the user. It can also do
    /// some light Hospital. Everything else view delegates
    /// to the controller.
    /// </summary>
    public partial class PatientAppointmentsOverview : Window, IObserver
    {
        private AppointmentController _controller;
        private DoctorController _doctorController;
        private Patient ActivePatient;
        public ObservableCollection<Appointment> Appointments { get; set; }
        public Appointment SelectedAppointment { get; set; }
        //public Patient ActiePatient;
        public PatientAppointmentsOverview()
        {
            InitializeComponent();
            DataContext = this;
            ActivePatient = new Patient(2, "novo", "ime", 0, 0);
            _doctorController = new DoctorController();
            _controller = new AppointmentController();
            _controller.Subscribe(this);

            Appointments = new ObservableCollection<Appointment>(_controller.GetPatientAppointments(ActivePatient.Id));
        }

        private void ShowCreateAppointmentWindow_Click(object sender, RoutedEventArgs e)
        {
            CreatePatientAppointment createAppointment = new CreatePatientAppointment(_controller, ActivePatient);
            createAppointment.Show();
        }

        private void ShowCreatePriorityAppointmentWindow_Click(object sender, RoutedEventArgs e)
        {
            CreatePriorityAppointment createPriorityAppointment = new CreatePriorityAppointment(_controller, _doctorController);
            createPriorityAppointment.Show();
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                MessageBoxResult result = ConfirmAppointmentDeletion();

                if (result == MessageBoxResult.Yes)
                {
                    if (++ActivePatient.ChangedAppointmentsCount > 5)
                    {
                        ActivePatient.IsBlocked = true;
                    }
                    _controller.Delete(SelectedAppointment);
                }
                
            }
            else
            {

                MessageBox.Show("Choose appointment you want to delete");
            }
        }
        private void ShowAnamnesis_Click(object sender, RoutedEventArgs eventArgs)
        {
            AnamnesisOverview anamnesisOverview = new AnamnesisOverview();
            anamnesisOverview.Show();
        }

        private void EditAppointmentButtonWindow_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                EditAppointment createAppointment=new EditAppointment(_controller, SelectedAppointment, ActivePatient);
                createAppointment.Show();
                //MessageBox.Show(SelectedAppointment.Patient);

            }
            else
            {

                MessageBox.Show("Choose appointment you want to modify");
            }
        }
        private MessageBoxResult ConfirmAppointmentDeletion()
        {
            string sMessageBoxText = $"Are you sure you want to delete this appointment\n{SelectedAppointment}";
            string sCaption = "Yes";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
            
            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }

        private void UpdateAppointmentList()
        {
            Appointments.Clear();
            foreach (var appointment in _controller.GetPatientAppointments(ActivePatient.Id))
            {
                Appointments.Add(appointment);
            }
        }

        public void Update()
        {
            UpdateAppointmentList();
        }
    }
}
