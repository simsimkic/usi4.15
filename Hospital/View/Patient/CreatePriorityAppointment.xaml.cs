using Hospital.Controller;
using Hospital.Model;
using Hospital.View.PatientView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class CreatePriorityAppointment : Window, INotifyPropertyChanged
    {
        private AppointmentController _controller;
        private DoctorController _doctorController;
        public Appointment Appointment { get; set; }

        public int IdDoctor { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public DateTime DueTo { get; set; }
        public Priority Priority { get; set; }
        public CreatePriorityAppointment(AppointmentController controller, DoctorController doctorController)
        {
            InitializeComponent();
            DataContext = this;
            _controller = controller;
            _doctorController = doctorController;
            DueTo = DateTime.Now;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void CreateAppointment_Click(object sender, RoutedEventArgs e)
        {

            TimeSlot freeTimeslot;
            if (Priority.Equals(Priority.Time))
            {
                freeTimeslot = _controller.FindFreeTimeSlot(IdDoctor, From, To, DueTo);
            }
            else
            {
                freeTimeslot = _controller.FindFreeTimeSlot(IdDoctor, DueTo);
            }

            if (freeTimeslot!= null) 
            {
                Appointment = new Appointment(111, IdDoctor, 2, false, freeTimeslot.Start, "",_doctorController.GetDoctor(IdDoctor).Specialization);
                _controller.Create(Appointment);
                    Close();
            }
            else
            {
                SelectPriorityAppointment selectPriorityAppointment = new SelectPriorityAppointment(_doctorController);
                selectPriorityAppointment.Show();
                Close();
            }
                
            
            }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
