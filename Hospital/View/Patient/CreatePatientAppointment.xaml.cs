using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Hospital.Controller;
using Hospital.Model;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class CreatePatientAppointment : Window, INotifyPropertyChanged
    {
        private AppointmentController _appointmentController;
        private DoctorController _doctorController;
        private Patient ActivePatient;
        public Appointment Appointment { get; set; }

        public CreatePatientAppointment(AppointmentController controller, Patient activePatient)
        {
            InitializeComponent();
            DataContext = this;

            ActivePatient = activePatient;
            Appointment = new Appointment();
            Appointment.IdPatient = activePatient.Id;

            _appointmentController = controller;
            _doctorController = new DoctorController();

        }

       


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (ActivePatient.IsBlocked)
            {
                MessageBox.Show("Patient is blocked");
                Close();
                return;
            }
            if (Appointment.IsAppointmentValid)
            {

                if (_appointmentController.IsAvailable(Appointment.IdDoctor, Appointment.TimeSlot))
                {
                    Appointment.IsOperation = false;
                    Appointment.Specialization = _doctorController.GetDoctor(Appointment.IdDoctor).Specialization;
                    _appointmentController.Create(Appointment);

                    if (++ActivePatient.CreatedAppointmentsCount > 8)
                    {
                        ActivePatient.IsBlocked = true;
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Doctor is not available");
                }
                //Close();
                
            }
            
            else
            {
                MessageBox.Show("Fields are not valid");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
