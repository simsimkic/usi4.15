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
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void AppointmentOveriew_Click(object sender, RoutedEventArgs e)
        {
            ChooseDate showAppointment = new ChooseDate();
            showAppointment.Show();
        }

        private void PatientOverview_Click(object sender, RoutedEventArgs e)
        {
            PatientOverview showPatient = new PatientOverview();
            showPatient.Show();
        }

        public void PatientAppointmentsOverview_Click(object sender, RoutedEventArgs e)
        {
            PatientAppointmentsOverview patientAppointmentsOverview = new PatientAppointmentsOverview();
            patientAppointmentsOverview.Show();
        }

        private void SearchWindow_Click(object sender, RoutedEventArgs e) {
            //SearchWindow adminWindow = new SearchWindow();
           // adminWindow.Show();
           
            AdminMain adminWindow = new AdminMain();
            adminWindow.Show();
        }

    }
}
