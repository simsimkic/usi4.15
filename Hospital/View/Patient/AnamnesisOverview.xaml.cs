using Hospital.Controller;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    
    public partial class AnamnesisOverview : Window
    {

        private AppointmentController _controller;
        public Patient ActivePatient { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public SortOption SelectedSortOption { get; set; }

        public AnamnesisOverview()
        {
            InitializeComponent();
            DataContext = this;

            ActivePatient = new Patient(2, "novo", "ime", 0, 0);
            _controller = new AppointmentController();
            Appointments = new ObservableCollection<Appointment>(_controller.GetPastAppointments(ActivePatient.Id));
        }
        private void SearchAnamnesis(object sender, TextChangedEventArgs e)
        {
            Appointments = new ObservableCollection<Appointment>(_controller.GetPastAppointments(ActivePatient.Id)
                .Where(a => a.Anamnesis.Contains(AnamnesisTxt.Text))
                .ToList());
            DataGrid dg = (DataGrid)FindName("AnamnesisDataGrid");
                dg.ItemsSource = Appointments;
        }
        private void OrderAnamnesis(object sender, SelectionChangedEventArgs e)
        {
            switch (SelectedSortOption)
            {
                case SortOption.Time:
                    Appointments = new ObservableCollection<Appointment>(Appointments.OrderBy(a => a.TimeSlot.Start).ToList());
                    break;
                case SortOption.Doctor:
                    Appointments = new ObservableCollection<Appointment>(Appointments.OrderBy(a => a.IdDoctor).ToList());
            break;
                case SortOption.Specialization:
                    Appointments = new ObservableCollection<Appointment>(Appointments.OrderBy(a => a.Specialization).ToList());
            break;
        }
            DataGrid dg = (DataGrid)FindName("AnamnesisDataGrid");
            dg.ItemsSource = Appointments;
        }
    }
}
