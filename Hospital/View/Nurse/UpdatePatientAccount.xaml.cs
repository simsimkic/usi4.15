using Hospital.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Hospital.Controller;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for UpdatePatientAccount.xaml
    /// </summary>
    public partial class UpdatePatient : Window, INotifyPropertyChanged
    {
        private PatientController _controller;

        public Patient Patient { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public UpdatePatient(PatientController controller,Patient patient)
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

        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
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
