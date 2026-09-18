using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos;
using Workout_Tracker.Helpers;
using Workout_Tracker.Models;

namespace Workout_Tracker.Inerface
{
    public interface IWorkoutPlanRepository
    {
        Task<WorkoutPlan?> GetByIdAsync(int id, string userId);
        Task<List<WorkoutPlan>> GetAllAsync(string userId, QueryObject query);
        Task<WorkoutPlan?> UpdateAsync(int id, UpdateWorkoutPlanRequestDto workoutPlanDto, string userId);
        Task<WorkoutPlan?> DeleteAsync(int id, string userId);
        Task<WorkoutPlan> CreateAsync(WorkoutPlan workoutplanModel);
    }
}