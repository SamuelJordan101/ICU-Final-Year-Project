using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Achievement
    {
        public int? Id { get; set; }
        public int PatientId { get; set; }
        public string Achievement1 { get; set; }
    }
}
