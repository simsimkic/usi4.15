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
using Hospital.Model;
namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for MedicalRecordView.xaml
    /// </summary>
    public partial class MedicalRecordView : Window
    {
        public MedicalRecord MedicalRecord { get; set; }
        public Patient Patient { get; set; }   
        public string RecordTitle { get; set; }
        public MedicalRecordView(Patient patient)
        {
            InitializeComponent();
            DataContext = this;
            RecordTitle = "Medical Record : " + patient.FirstName + " "+ patient.LastName;
            MedicalRecord = patient.MedicalRecord;
        }
    }
}
