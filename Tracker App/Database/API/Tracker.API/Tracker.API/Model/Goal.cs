using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Goal
    {
        public int? Id { get; set; }
        public int PatientId { get; set; }
        public string Goal1 { get; set; }
        public bool Assigned { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? Done { get; set; }
    }
}
