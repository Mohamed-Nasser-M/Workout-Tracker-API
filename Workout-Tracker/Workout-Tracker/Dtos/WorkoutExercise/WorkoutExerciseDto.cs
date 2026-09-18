using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker.Dtos.WorkoutExercise
{
    public class WorkoutExerciseDto
    {
        public int Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}