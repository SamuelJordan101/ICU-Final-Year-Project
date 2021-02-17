using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Patient
    {
        public Patient()
        {
        }

        public int PatientId { get; set; }
        public string Name { get; set; }
        public DateTime Admission { get; set; }
        public string Ward { get; set; }
        public string Hospital { get; set; }
        public int? GoalCpax { get; set; }
    }
}
