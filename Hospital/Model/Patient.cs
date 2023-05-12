using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hospital.Serializer;
using Newtonsoft.Json;

namespace Hospital.Model
{
    public class Patient: INotifyPropertyChanged,IDataErrorInfo
    {

        public int Id { get; set; }




        private bool _isBlocked;
        public bool IsBlocked
        {
            get => _isBlocked;
            set
            {
                if (value != _isBlocked)
                {
                    _isBlocked = value;
                    OnPropertyChanged();
                }
            }
        }


        private int _createdAppointmentsCount;
        public int CreatedAppointmentsCount
        {
            get => _createdAppointmentsCount;
            set
            {
                if (value != _createdAppointmentsCount)
                {
                    _createdAppointmentsCount = value;
                    OnPropertyChanged();
                }
            }
        }



        private int _changedAppointmentsCount;
        public int ChangedAppointmentsCount
        {
            get => _changedAppointmentsCount;
            set
            {
                if (value != _changedAppointmentsCount)
                {
                    _changedAppointmentsCount = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private MedicalRecord _medicalRecord;
        public MedicalRecord MedicalRecord
        {
            get => _medicalRecord;
            set
            {
                if (value != _medicalRecord)
                {
                    _medicalRecord = value;
                    OnPropertyChanged();
                }
            }
        }


        public Patient() {
            Id = 0;
            FirstName = "";
            LastName = "";
            ChangedAppointmentsCount = 0;
            CreatedAppointmentsCount = 0;
            IsBlocked = false;
            MedicalRecord = new MedicalRecord();
        }

        public Patient(int patientId, string firstName, string lastName, int changedAppointments, int createdAppointments)
        {
            Id = patientId;
            FirstName = firstName;
            LastName = lastName;
            ChangedAppointmentsCount = changedAppointments;
            CreatedAppointmentsCount = createdAppointments;
            IsBlocked = false;
            MedicalRecord = new MedicalRecord();
        }


        //Konstruktor ako dodje do greske
        //public Patient(int id, string firstName, string lastName)
        //{
        //    Id = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //}

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        [JsonIgnore]
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(FirstName))
                        return "First name is required";
                }
                else if (columnName == "LastName")
                {
                    if (string.IsNullOrEmpty(LastName))
                        return "Last name is required";
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = {"FirstName", "LastName" };

        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return MedicalRecord.IsValid;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
