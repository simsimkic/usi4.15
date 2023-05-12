using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Hospital.Controller;
using Hospital.Model;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for CreatePatientAccount.xaml
    /// </summary>
    public partial class CreatePatient : Window,INotifyPropertyChanged
    {
        private PatientController _controller;

        public Patient Patient{ get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        public CreatePatient(PatientController controller)
        {
            InitializeComponent();
            DataContext = this;
            Patient = new Patient();
            MedicalRecord = new MedicalRecord();
            

            _controller = controller;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreatePatient_Click(object sender, RoutedEventArgs e)
        {
            Patient.MedicalRecord = MedicalRecord;

            if (Patient.IsValid)
            {

                _controller.Create(Patient);
                Close();
            }
            else
            {
                MessageBox.Show("Unable to create, invalid fields");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
