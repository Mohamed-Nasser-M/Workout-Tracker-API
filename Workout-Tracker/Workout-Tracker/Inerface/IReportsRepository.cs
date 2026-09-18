using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos.Reports;
using Workout_Tracker.Models;

namespace Workout_Tracker.Inerface
{
    public interface IReportsRepository
    {
        Task<List<WorkoutSession>> GetWorkoutHistoryAsync(string userId);
        Task<WorkoutSummaryDto> GetWorkoutSummaryAsync(string userId);
        Task<List<ExerciseProgressDto>> GetExerciseProgressAsync(string userId);
    }
}