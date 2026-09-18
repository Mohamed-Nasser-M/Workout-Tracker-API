using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker.Dtos.WorkoutSessionExercise
{
    public class UpdateWorkoutSessionExerciseRequestDto
    {
        [Range(1,1000)]
        public int? ActualSets { get; set; }
        [Range(1,1000)]
        public int? ActualReps { get; set; }
        [Range(0,1000)]
        public decimal? ActualWeight { get; set; }
        [MaxLength(1000, ErrorMessage = "Notes cannot be over 1000 characters")]
        public string? Notes { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}