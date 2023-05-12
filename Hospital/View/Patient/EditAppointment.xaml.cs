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
    public partial class EditAppointment : Window, INotifyPropertyChanged
    {
        private AppointmentController _controller;

        private Patient ActivePatient;
        public Appointment Appointment { get; set; }

        

        public EditAppointment(AppointmentController controller, Appointment appointment, Patient activePatient)
        {
            InitializeComponent();
            DataContext = this;

            ActivePatient = activePatient;
            Appointment = appointment;

            _controller = controller;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (Appointment.IsAppointmentValid && !ActivePatient.IsBlocked)
            {
                _controller.Update();
                if (++ActivePatient.ChangedAppointmentsCount>5)
                {
                    ActivePatient.IsBlocked = true;
                }
                Close();
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
