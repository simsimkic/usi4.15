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
    /// Interaction logic for ChooseDate.xaml
    /// </summary>
    public partial class ChooseDate : Window
    {
        public ChooseDate()
        {
            InitializeComponent();
        }

        private void AppointmentOverviewOnDate_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = datePicker.SelectedDate;
            if (selectedDate != null)
            {
                DateTime date = selectedDate.GetValueOrDefault();
                MessageBox.Show("Izabrani datum je: " + selectedDate.Value.ToString("dd.MM.yyyy."));
                AppointmentOverview appointmentOverview = new AppointmentOverview(date);
                appointmentOverview.Show();
            }
            else
            {
                MessageBox.Show("Date not entered.");
            }
        }
    }

    
}
