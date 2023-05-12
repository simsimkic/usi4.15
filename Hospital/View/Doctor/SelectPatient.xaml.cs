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
    /// Interaction logic for SelectPatient.xaml
    /// </summary>
    public partial class SelectPatient : Window
    {
        public Patient SelectedPatient { get; set; }
        public SelectPatient()
        {
            InitializeComponent();
        }

        private void ShowMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                MedicalRecordView medicalRecordView = new MedicalRecordView(SelectedPatient);
                medicalRecordView.Show();
            }

        }
    }
}
