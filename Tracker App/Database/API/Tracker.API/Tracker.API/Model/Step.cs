using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Step
    {
        public Step()
        {
            Exercises = new HashSet<Exercise>();
        }

        public int Id { get; set; }
        public string Step1 { get; set; }
        public int Image { get; set; }

        public virtual Image ImageNavigation { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
