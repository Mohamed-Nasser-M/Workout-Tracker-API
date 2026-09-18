using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker.Models
{
    public class WorkoutSessionExercise
    {
        public int Id { get; set; }
        public int? ActualSets { get; set; }
        public int? ActualReps { get; set; }
        public decimal? ActualWeight { get; set; }
        public string? Notes { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public int WorkoutSessionId { get; set; }
        public int ExerciseId { get; set; }
        public WorkoutSession WorkoutSession { get; set; } = null!;
        public Exercise Exercise { get; set; } = null!;
    }
}