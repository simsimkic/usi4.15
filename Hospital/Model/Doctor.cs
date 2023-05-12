using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Model;

namespace Hospital.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        private string _firstName;

        public string FirstName { get; set; }

        private string _lastName;

        public string LastName { get; set; }

        private Specialization _specialization;
        public Specialization Specialization { get; set; }

        public Doctor() { }

        public Doctor(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Doctor(int idDoctor, string firstName, string lastName, Specialization spec)
        {
            Id = idDoctor;
            FirstName = firstName;
            LastName = lastName;
            Specialization = spec;
        }
    }
}
