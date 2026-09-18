using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos;
using Workout_Tracker.Models;

namespace Workout_Tracker.Inerface
{
    public interface IWorkoutSessionRepository
    {
        Task<WorkoutSession?> GetByIdAsync(int id, string userId);
        Task<List<WorkoutSession>> GetAllAsync(string userId);
        Task<WorkoutSession?> UpdateAsync(int id, UpdateWorkoutSessionRequestDto WorkoutSessionDto, string userId);
        Task<WorkoutSession?> CreateAsync(WorkoutSession WorkoutSessionModel, string userId);
    }
}