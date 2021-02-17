using System;
using System.Collections.Generic;

#nullable disable

namespace Tracker.API.Model
{
    public partial class Patient
    {
        public Patient()
        {
            Achievements = new HashSet<Achievement>();
            ExercisePlans = new HashSet<ExercisePlan>();
            Goals = new HashSet<Goal>();
        }

        public int PatientId { get; set; }
        public string Name { get; set; }
        public DateTime Admission { get; set; }
        public string Ward { get; set; }
        public string Hospital { get; set; }
        public int? GoalCpax { get; set; }

        public virtual Cpax GoalCpaxNavigation { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<ExercisePlan> ExercisePlans { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
    }
}
