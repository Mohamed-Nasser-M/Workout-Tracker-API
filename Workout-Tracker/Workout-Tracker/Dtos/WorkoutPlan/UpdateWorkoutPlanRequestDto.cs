
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Dtos
{
    public class UpdateWorkoutPlanRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must be 3 characters")]
        [MaxLength(100, ErrorMessage = "Title cannot be over 100 characters")]
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        [Range(1,3)]
        public WorkoutStatus Status { get; set; }
    }
}