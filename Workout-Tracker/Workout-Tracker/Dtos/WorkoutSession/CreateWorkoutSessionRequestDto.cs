using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Dtos
{
    public class CreateWorkoutSessionRequestDto
    {
        public DateTime StartedAt { get; set; }
        [Required]
        [Range(1,4)]
        public SessionStatus Status { get; set; }
        [MaxLength(1000, ErrorMessage = "Notes cannot be over 1000 characters")]
        public string? Notes { get; set; } = string.Empty;
        [Required]
        [Range(1,1000000000)]
        public int WorkoutPlanId { get; set; }
    }
}