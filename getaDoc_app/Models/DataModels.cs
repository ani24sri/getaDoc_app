using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getaDoc_app.Models
{
    public enum Availibility
    {
        Available,
        Unavailable
    }
    public class DoctorsModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string speciality { get; set; }
        public double phoneno { get; set; }
    }
    public class PatientsModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symptoms { get; set; }
        public Int64 patientNo { get; set; }
        public Int64 phNo { get; set; }
    }
    public class Appointments
    {
        public int id { get; set; }
        public DateTime appDate { get; set; }
        public string reason { get; set; }
        public Availibility availble { get; set; }
    }
        public class diseaseData
        {
            public int id { get; set; }
            public string name { get; set; }
            public string symptom1 { get; set; }
            public string symptom2 { get; set; }
            public string symptom3 { get; set; }
            public string symptom4 { get; set; }
            public string desc { get; set; }
            public string cure { get; set; }
        }
    }

