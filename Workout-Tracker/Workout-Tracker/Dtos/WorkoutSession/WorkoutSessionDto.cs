using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Dtos
{
    public class WorkoutSessionDto
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int DurationMinutes { get; set; }
        public SessionStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int WorkoutPlanId { get; set; }
    }
}