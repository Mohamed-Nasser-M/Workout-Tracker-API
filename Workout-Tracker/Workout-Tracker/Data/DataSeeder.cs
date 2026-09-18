using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;
using Workout_Tracker.Models;

namespace Workout_Tracker.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDBContext context)
        {
            if (context.Exercises.Any())
            {
                return;
            }

            var exercises = new List<Exercise>
            {
                new Exercise
                {
                    Name = "Bench Press",
                    Description = "A chest exercise performed using a barbell.",
                    Category = ExerciseCategory.Strength,
                    MuscleGroup = MuscleGroup.Chest
                },
                new Exercise
                {
                    Name = "Squat",
                    Description = "A lower-body exercise performed by bending the knees and hips.",
                    Category = ExerciseCategory.Strength,
                    MuscleGroup = MuscleGroup.Legs
                },
            };

            context.Exercises.AddRange(exercises);
            await context.SaveChangesAsync();
        }
    }
}