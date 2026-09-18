using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker.Data;
using Workout_Tracker.Dtos;
using Workout_Tracker.Enums;
using Workout_Tracker.Helpers;
using Workout_Tracker.Inerface;
using Workout_Tracker.Models;

namespace Workout_Tracker.Repository
{
    public class WorkoutPlanRepository : IWorkoutPlanRepository
    {
        public readonly ApplicationDBContext _context;
        public WorkoutPlanRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<WorkoutPlan> CreateAsync(WorkoutPlan workoutplanModel)
        {
            await _context.WorkoutPlans.AddAsync(workoutplanModel);
            await _context.SaveChangesAsync();
            return workoutplanModel;
        }

        public async Task<WorkoutPlan?> DeleteAsync(int id, string userId)
        {
            var workoutPlanModel = await _context.WorkoutPlans.FirstOrDefaultAsync(w => w.Id == id && w.AppUserId == userId);

            if(workoutPlanModel == null)
            {
                return null;
            }

            _context.WorkoutPlans.Remove(workoutPlanModel);
            await _context.SaveChangesAsync();
            return workoutPlanModel;
        }

        public async Task<List<WorkoutPlan>> GetAllAsync(string userId, QueryObject query)
        {
            var workoutPlans = _context.WorkoutPlans.Where(w => w.AppUserId == userId).AsQueryable();

            if (query.Status.HasValue)
            {
                workoutPlans = workoutPlans
                    .Where(w => w.Status == query.Status.Value);
            }
            else
            {
                workoutPlans = workoutPlans
                    .Where(w => w.Status == WorkoutStatus.Pending);
            }

            workoutPlans = query.IsDecsending
                ? workoutPlans.OrderByDescending(w => w.ScheduledDate)
                : workoutPlans.OrderBy(w => w.ScheduledDate);

            return await workoutPlans.ToListAsync();
        }

        public async Task<WorkoutPlan?> GetByIdAsync(int id, string userId)
        {
            return await _context.WorkoutPlans.Where(w => w.AppUserId == userId).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<WorkoutPlan?> UpdateAsync(int id, UpdateWorkoutPlanRequestDto workoutPlanDto, string userId)
        {
            var existentWorkoutPlan = await _context.WorkoutPlans.Where(w => w.AppUserId == userId).FirstOrDefaultAsync(x => x.Id == id);

            if(existentWorkoutPlan == null)
            {
                return null;
            }

            existentWorkoutPlan.Title = workoutPlanDto.Title;
            existentWorkoutPlan.ScheduledDate = workoutPlanDto.ScheduledDate;
            existentWorkoutPlan.Status = workoutPlanDto.Status;

            await _context.SaveChangesAsync();

            return existentWorkoutPlan;
        }
    }
}