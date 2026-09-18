using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int WorkoutPlanId { get; set; }
        public int ExerciseId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; } = null!;
        public Exercise Exercise { get; set; } = null!;
    }
}