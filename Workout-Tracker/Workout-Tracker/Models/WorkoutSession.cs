using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Models
{
    public class WorkoutSession
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int DurationMinutes { get; set; }
        public SessionStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int WorkoutPlanId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; } = null!;
        public List<WorkoutSessionExercise> WorkoutSessionExercises { get; set; } = new();
    }
}