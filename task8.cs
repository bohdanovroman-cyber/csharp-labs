using System;

namespace ClinicApp
{
    public class GrowablePatientManager
    {
        private Patient[] _patients = new Patient[2];
        private int _count = 0;

        public void Add(Patient patient)
        {
            if (_count == _patients.Length)
            {
                Patient[] newArray = new Patient[_patients.Length * 2];
                for (int i = 0; i < _count; ++i) newArray[i] = _patients[i];
                _patients = newArray;
            }
            _patients[_count++] = patient;
        }

        public void DisplayAll()
        {
            for (int i = 0; i < _count; ++i) Console.WriteLine(_patients[i]);
        }
    }
}