using Hospital.Controller;
using Hospital.Model;
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

namespace Hospital.View.PatientView
{
    
    public partial class SelectPriorityAppointment : Window
    {
        private AppointmentController _controller;
        private DoctorController _doctorController;
        public Patient ActivePatient { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public Appointment SelectedAppointment { get; set; }

        public SelectPriorityAppointment(DoctorController doctorController)
        {
            InitializeComponent();
            DataContext = this;

            ActivePatient = new Patient();
            _controller = new AppointmentController();
            _doctorController = doctorController;

            Appointments = new ObservableCollection<Appointment>(_controller.FindSuggestedAppointment(_doctorController.GetAllDoctors()));
        }
        private void CreateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment == null)
            {
                MessageBox.Show("Not choosen");
            }
            else
            {
                _controller.Create(SelectedAppointment);
                _controller.Update();
                MessageBox.Show("Appointment created");
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
