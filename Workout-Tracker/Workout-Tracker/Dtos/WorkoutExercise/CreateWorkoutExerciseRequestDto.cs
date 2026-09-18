using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker.Dtos.WorkoutExercise
{
    public class CreateWorkoutExerciseRequestDto
    {
        [Required]

        public int ExerciseId { get; set; }
        [Required]
        [Range(1,100)]
        public int Sets { get; set; }
        [Required]
        [Range(1,100)]
        public int Reps { get; set; }
        [Required]
        [Range(0,300)]
        public decimal Weight { get; set; }
        [MaxLength(1000, ErrorMessage = "Notes cannot be over 1000 characters")]
        public string Notes { get; set; } = string.Empty;
    }
}