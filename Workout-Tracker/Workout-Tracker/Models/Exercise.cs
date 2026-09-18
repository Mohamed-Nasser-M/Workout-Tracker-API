using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ExerciseCategory Category { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public List<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
        public List<WorkoutSessionExercise> WorkoutSessionExercises { get; set; } = new List<WorkoutSessionExercise>();
    }
}