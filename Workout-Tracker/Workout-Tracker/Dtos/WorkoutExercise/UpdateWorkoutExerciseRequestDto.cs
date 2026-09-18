using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker.Dtos.WorkoutExercise
{
    public class UpdateWorkoutExerciseRequestDto
    {
        [Range(1,100)]
        public int Sets { get; set; }
        [Range(1,100)]
        public int Reps { get; set; }
        [Range(1,300)]
        public decimal Weight { get; set; }
        [MaxLength(1000, ErrorMessage = "Notes cannot be over 1000 characters")]
        public string Notes { get; set; } = string.Empty;
    }
}