using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Step
    {
        public Step()
        {
        }

        public int Id { get; set; }
        public string Step1 { get; set; }
        public int Image { get; set; }
        public int ExerciseID { get; set; }
    }
}
