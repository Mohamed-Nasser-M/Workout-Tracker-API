using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos;
using Workout_Tracker.Models;

namespace Workout_Tracker.Mappers
{
    public static class ExerciseMapper
    {
        public static ExerciseDto ToExerciseDto(this Exercise exerciseModel)
        {
            return new ExerciseDto
            {
                Id = exerciseModel.Id,
                Name = exerciseModel.Name,
                Description = exerciseModel.Description,
                Category = exerciseModel.Category,
                MuscleGroup = exerciseModel.MuscleGroup
            };
        }
    }
}