using Hospital.Controller;
using Hospital.Model;
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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentDoctor.xaml
    /// </summary>
    public partial class UpdateAppointmentDoctor : Window
    {

        private AppointmentController _controller;

        public Appointment Appointments { get; set; }
        public UpdateAppointmentDoctor(AppointmentController controller, Appointment appointment)
        {
            InitializeComponent();
            DataContext = this;
            _controller = controller;
            Appointments = appointment;
        }

        private void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            
            if (Appointments.IsValid == "")
            {

                _controller.Update();
                Close();
            }
            else
            {
                MessageBox.Show("Unable to update, invalid fields.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Close();
            }
        }
    }
}
