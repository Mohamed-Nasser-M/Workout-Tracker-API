using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos.WorkoutExercise;
using Workout_Tracker.Models;

namespace Workout_Tracker.Inerface
{
    public interface IWorkoutExerciseRepository
    {
        Task<List<WorkoutExercise>> GetAllAsync(int workoutPlanId, string userId);
        Task<WorkoutExercise?> UpdateAsync(int workoutPlanId, int id, UpdateWorkoutExerciseRequestDto WorkoutExerciseDto, string userId);
        Task<WorkoutExercise?> DeleteAsync(int workoutPlanId, int id, string userId);
        Task<WorkoutExercise?> CreateAsync(WorkoutExercise workoutexerciseModel, string userId);
    }
}