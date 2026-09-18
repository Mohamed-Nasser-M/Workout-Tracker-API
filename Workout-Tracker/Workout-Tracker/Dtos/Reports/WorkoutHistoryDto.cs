using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Dtos.Reports
{
    public class WorkoutHistoryDto
    {
        public int SessionId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int DurationMinutes { get; set; }
        public SessionStatus Status { get; set; }
    }
}