using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker.Dtos.Reports
{
    public class ExerciseProgressDto
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public int? ActualSets { get; set; }
        public int? ActualReps { get; set; }
        public decimal? ActualWeight { get; set; }
    }
}