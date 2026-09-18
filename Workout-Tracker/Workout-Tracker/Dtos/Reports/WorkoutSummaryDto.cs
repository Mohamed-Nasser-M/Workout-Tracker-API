using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker.Dtos.Reports
{
    public class WorkoutSummaryDto
    {
        public int TotalWorkouts { get; set; }
        public int CompletedWorkouts { get; set; }
        public int TotalDurationMinutes { get; set; }
        public int TotalExercisesCompleted { get; set; }
    }
}