using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Exercise
    {
        public Exercise()
        {
            ExercisePlans = new HashSet<ExercisePlan>();
        }

        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public string Category { get; set; }
        public int StepId { get; set; }
        public int Image { get; set; }

        public virtual Image ImageNavigation { get; set; }
        public virtual Step Step { get; set; }
        public virtual ICollection<ExercisePlan> ExercisePlans { get; set; }
    }
}
