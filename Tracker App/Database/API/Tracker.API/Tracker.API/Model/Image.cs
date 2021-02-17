using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Image
    {
        public Image()
        {
            Exercises = new HashSet<Exercise>();
            Steps = new HashSet<Step>();
        }

        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public string Category { get; set; }
        public int? PatientID { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
    }
}
