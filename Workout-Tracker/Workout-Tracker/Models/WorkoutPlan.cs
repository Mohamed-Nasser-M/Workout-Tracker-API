using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Models
{
    public class WorkoutPlan
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        public WorkoutStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
        public List<WorkoutSession> WorkoutSessions { get; set; } = new List<WorkoutSession>();
    }
}