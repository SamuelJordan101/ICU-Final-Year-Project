using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Cpax
    {
        public Cpax()
        {
        }

        public int Id { get; set; }
        public int PatientID { get; set; }
        public DateTime CPAXDate { get; set; }
        public int Grip { get; set; }
        public int Respiratory { get; set; }
        public int Cough { get; set; }
        public int BedMovement { get; set; }
        public int DynamicSitting { get; set; }
        public int StandingBalance { get; set; }
        public int SitToStand { get; set; }
        public int BedToChair { get; set; }
        public int Stepping { get; set; }
        public int Transfer { get; set; }
    }
}
