using Hospital.Controller;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for EditMedicalRecord.xaml
    /// </summary>
    public partial class EditMedicalRecord : Window
    {

        private PatientController _controller;

        public Patient Patient { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        public EditMedicalRecord(PatientController controller, Patient patient)
        {
            InitializeComponent();
            DataContext = this;
            _controller = controller;
            Patient = patient;
            MedicalRecord = patient.MedicalRecord;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateRecord_Click(object sender, RoutedEventArgs e)
        {
            Patient.MedicalRecord = MedicalRecord;
            if (Patient.IsValid)
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
            Close();
        }

    }


}
