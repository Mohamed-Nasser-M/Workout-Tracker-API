using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Dtos
{
    public class UpdateWorkoutSessionRequestDto
    {
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        [Range(1,1000)]
        public int DurationMinutes { get; set; }
        [Range(1,4)]
        public SessionStatus Status { get; set; }
        [MaxLength(1000, ErrorMessage = "Notes cannot be over 1000 characters")]
        public string? Notes { get; set; } = string.Empty;
    }
}