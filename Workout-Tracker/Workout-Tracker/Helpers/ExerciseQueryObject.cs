using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Helpers
{
    public class ExerciseQueryObject
    {
        public ExerciseCategory? Category { get; set; } = null;
        public MuscleGroup? MuscleGroup { get; set; } = null;
    }
}