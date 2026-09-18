using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Dtos;
using Workout_Tracker.Models;

namespace Workout_Tracker.Mappers
{
    public static class WorkoutPlanMapper
    {
        public static WorkoutPlanDto ToWorkoutPlanDto(this WorkoutPlan WorkoutPlanModel)
        {
            return new WorkoutPlanDto
            {
                Id = WorkoutPlanModel.Id,
                Title = WorkoutPlanModel.Title,
                ScheduledDate = WorkoutPlanModel.ScheduledDate,
                Status = WorkoutPlanModel.Status
            };
        }

        public static WorkoutPlan ToWorkoutPlanFromCreateDto(this CreateWorkoutPlanRequestDto WorkoutPlanModel, string userId)
        {
            return new WorkoutPlan
            {
                AppUserId = userId,
                Title = WorkoutPlanModel.Title,
                ScheduledDate = WorkoutPlanModel.ScheduledDate,
                Status = WorkoutPlanModel.Status
            };
        }
    }
}