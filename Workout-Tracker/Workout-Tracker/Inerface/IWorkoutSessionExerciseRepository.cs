using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos.WorkoutSessionExercise;
using Workout_Tracker.Models;

namespace Workout_Tracker.Inerface
{
    public interface IWorkoutSessionExerciseRepository
    {
        Task<List<WorkoutSessionExercise>> GetAllAsync(int sessionId, string userId);
        Task<WorkoutSessionExercise?> UpdateAsync(int sessionId, int id, UpdateWorkoutSessionExerciseRequestDto WorkoutSessionExerciseDto, string userId);
    }
}