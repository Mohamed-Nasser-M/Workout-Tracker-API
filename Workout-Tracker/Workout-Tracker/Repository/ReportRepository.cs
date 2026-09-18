using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker.Data;
using Workout_Tracker.Dtos.Reports;
using Workout_Tracker.Enums;
using Workout_Tracker.Inerface;
using Workout_Tracker.Models;

namespace Workout_Tracker.Repository
{
    public class ReportRepository : IReportsRepository
    {
        private readonly ApplicationDBContext _context;
        public ReportRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseProgressDto>> GetExerciseProgressAsync(string userId)
        {
            return await _context.WorkoutSessionExercises.Where(
                w => w.WorkoutSession.WorkoutPlan.AppUserId == userId && w.IsCompleted)
                .Select(w => new ExerciseProgressDto
                {
                    ExerciseId = w.ExerciseId,
                    ExerciseName = w.Exercise.Name,

                    Date = w.WorkoutSession.StartedAt,

                    ActualSets = w.ActualSets,
                    ActualReps = w.ActualReps,
                    ActualWeight = w.ActualWeight
                })
                .OrderBy(w => w.ExerciseId).ThenBy(w => w.Date).ToListAsync();
        }

        public Task<List<WorkoutSession>> GetWorkoutHistoryAsync(string userId)
        {
            return _context.WorkoutSessions.Where(w => w.WorkoutPlan.AppUserId == userId).ToListAsync();
        }

        public async Task<WorkoutSummaryDto> GetWorkoutSummaryAsync(string userId)
        {
            var totalWorkouts = await _context.WorkoutPlans.Where(w => w.AppUserId == userId).CountAsync();

            var completedWorkouts = await _context.WorkoutPlans.Where(
                w => w.Status == WorkoutStatus.Completed && w.AppUserId == userId).CountAsync();

            var totalDurationMinutes = await _context.WorkoutSessions.Where(
                w => w.WorkoutPlan.AppUserId == userId).SumAsync(w => w.DurationMinutes);

            var totalExercisesCompleted = await _context.WorkoutSessionExercises.Where(
                w => w.WorkoutSession.WorkoutPlan.AppUserId == userId && w.IsCompleted).CountAsync();

            return new WorkoutSummaryDto
            {
                TotalWorkouts = totalWorkouts,
                CompletedWorkouts = completedWorkouts,
                TotalDurationMinutes = totalDurationMinutes,
                TotalExercisesCompleted = totalExercisesCompleted
            };
        }
    }
}