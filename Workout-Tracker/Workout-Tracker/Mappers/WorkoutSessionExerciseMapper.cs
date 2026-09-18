using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos.WorkoutSessionExercise;
using Workout_Tracker.Models;

namespace Workout_Tracker.Mappers
{
    public static class WorkoutSessionExerciseMapper
    {
        public static WorkoutSessionExerciseDto ToWorkoutSessionExerciseDto(this WorkoutSessionExercise WorkoutSessionModel)
        {
            return new WorkoutSessionExerciseDto
            {
                Id = WorkoutSessionModel.Id,
                ActualSets = WorkoutSessionModel.ActualSets,
                ActualReps = WorkoutSessionModel.ActualReps,
                ActualWeight = WorkoutSessionModel.ActualWeight,
                Notes = WorkoutSessionModel.Notes,
                IsCompleted = WorkoutSessionModel.IsCompleted,
                WorkoutSessionId = WorkoutSessionModel.WorkoutSessionId,
                ExerciseId = WorkoutSessionModel.ExerciseId
            };
        }
    }
}