using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos;
using Workout_Tracker.Dtos.Reports;
using Workout_Tracker.Models;

namespace Workout_Tracker.Mappers
{
    public static class WorkoutSessionMapper
    {
        public static WorkoutSessionDto ToWorkoutSessionDto(this WorkoutSession WorkoutSessionModel)
        {
            return new WorkoutSessionDto
            {
                Id = WorkoutSessionModel.Id,
                StartedAt = WorkoutSessionModel.StartedAt,
                FinishedAt = WorkoutSessionModel.FinishedAt,
                DurationMinutes = WorkoutSessionModel.DurationMinutes,
                Status = WorkoutSessionModel.Status,
                Notes = WorkoutSessionModel.Notes,
                WorkoutPlanId = WorkoutSessionModel.WorkoutPlanId
            };
        }
        public static WorkoutSession ToWorkoutSessionFromCreateDto(this CreateWorkoutSessionRequestDto WorkoutSessionModel)
        {
            return new WorkoutSession
            {
                StartedAt = WorkoutSessionModel.StartedAt,
                Status = WorkoutSessionModel.Status,
                Notes = WorkoutSessionModel.Notes,
                WorkoutPlanId = WorkoutSessionModel.WorkoutPlanId
            };
        }
        public static WorkoutHistoryDto ToWorkoutHistoryDto(this WorkoutSession WorkoutSessionModel)
        {
            return new WorkoutHistoryDto
            {
                SessionId = WorkoutSessionModel.Id,
                StartedAt = WorkoutSessionModel.StartedAt,
                FinishedAt = WorkoutSessionModel.FinishedAt,
                DurationMinutes = WorkoutSessionModel.DurationMinutes,
                Status = WorkoutSessionModel.Status
            };
        }
    }
}