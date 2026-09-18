using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker.Data;
using Workout_Tracker.Dtos;
using Workout_Tracker.Inerface;
using Workout_Tracker.Models;

namespace Workout_Tracker.Repository
{
    public class WorkoutSessionRepository : IWorkoutSessionRepository
    {
        public readonly ApplicationDBContext _context;
        public WorkoutSessionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<WorkoutSession?> CreateAsync(WorkoutSession WorkoutSessionModel, string userId)
        {
            var workoutPlan = await _context.WorkoutPlans.Include(w => w.WorkoutExercises).FirstOrDefaultAsync(
                w => w.Id == WorkoutSessionModel.WorkoutPlanId && w.AppUserId == userId);
            
            if(workoutPlan == null)
            {
                return null;
            }
            WorkoutSessionModel.WorkoutPlan = workoutPlan;
            
            var WorkoutExercises = workoutPlan.WorkoutExercises;

            foreach (var WorkoutExercise in WorkoutExercises)
            {
                var WorkoutSessionExercise = new WorkoutSessionExercise
                {
                    ExerciseId = WorkoutExercise.ExerciseId,
                    WorkoutSession = WorkoutSessionModel
                };
                
                await _context.WorkoutSessionExercises.AddAsync(WorkoutSessionExercise);
            }
            
            await _context.WorkoutSessions.AddAsync(WorkoutSessionModel);
            await _context.SaveChangesAsync();

            return WorkoutSessionModel;
        }

        public async Task<List<WorkoutSession>> GetAllAsync(string userId)
        {
            return await _context.WorkoutSessions.Where(w => w.WorkoutPlan.AppUserId == userId).ToListAsync();
        }

        public async Task<WorkoutSession?> GetByIdAsync(int id, string userId)
        {
            return await _context.WorkoutSessions.Where(w => w.WorkoutPlan.AppUserId == userId).FirstOrDefaultAsync(s => s.Id == id);;
        }

        public async Task<WorkoutSession?> UpdateAsync(int id, UpdateWorkoutSessionRequestDto WorkoutSessionDto, string userId)
        {
            var existentWorkoutSession = await _context.WorkoutSessions.Where(w => w.WorkoutPlan.AppUserId == userId).FirstOrDefaultAsync(x => x.Id == id);

            if(existentWorkoutSession == null)
            {
                return null;
            }

            existentWorkoutSession.StartedAt = WorkoutSessionDto.StartedAt;
            existentWorkoutSession.FinishedAt = WorkoutSessionDto.FinishedAt;
            existentWorkoutSession.DurationMinutes = WorkoutSessionDto.DurationMinutes;
            existentWorkoutSession.Status = WorkoutSessionDto.Status;
            existentWorkoutSession.Notes = WorkoutSessionDto.Notes;

            await _context.SaveChangesAsync();

            return existentWorkoutSession;
        }
    }
}