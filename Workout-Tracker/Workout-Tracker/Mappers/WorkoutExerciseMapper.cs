using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos.WorkoutExercise;
using Workout_Tracker.Models;

namespace Workout_Tracker.Mappers
{
    public static class WorkoutExerciseMapper
    {
        public static WorkoutExerciseDto ToWorkoutExerciseDto(this WorkoutExercise WorkoutExerciseModel)
        {
            return new WorkoutExerciseDto
            {
                Id = WorkoutExerciseModel.Id,
                Sets = WorkoutExerciseModel.Sets,
                Reps = WorkoutExerciseModel.Reps,
                Weight = WorkoutExerciseModel.Weight,
                Notes = WorkoutExerciseModel.Notes
            };
        }

        public static WorkoutExercise ToWorkoutExerciseFromCreateDto(this CreateWorkoutExerciseRequestDto WorkoutExerciseModel, int workoutPlanId)
        {
            return new WorkoutExercise
            {
                WorkoutPlanId = workoutPlanId,
                ExerciseId = WorkoutExerciseModel.ExerciseId,
                Sets = WorkoutExerciseModel.Sets,
                Reps = WorkoutExerciseModel.Reps,
                Weight = WorkoutExerciseModel.Weight,
                Notes = WorkoutExerciseModel.Notes
            };
        }
    }
}