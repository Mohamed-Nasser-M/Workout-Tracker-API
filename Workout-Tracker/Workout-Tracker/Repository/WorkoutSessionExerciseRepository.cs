using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker.Data;
using Workout_Tracker.Dtos.WorkoutSessionExercise;
using Workout_Tracker.Inerface;
using Workout_Tracker.Models;

namespace Workout_Tracker.Repository
{
    public class WorkoutSessionExerciseRepository : IWorkoutSessionExerciseRepository
    {
        public readonly ApplicationDBContext _context;
        public WorkoutSessionExerciseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutSessionExercise>> GetAllAsync(int sessionId, string userId)
        {
            return await _context.WorkoutSessionExercises.Where(x => 
            x.WorkoutSessionId == sessionId && x.WorkoutSession.WorkoutPlan.AppUserId == userId).ToListAsync();
        }

        public async Task<WorkoutSessionExercise?> UpdateAsync(int sessionId, int id, UpdateWorkoutSessionExerciseRequestDto WorkoutSessionExerciseDto, string userId)
        {
            var existentWorkoutSessionExercise = await _context.WorkoutSessionExercises.FirstOrDefaultAsync(x =>
            x.Id == id &&
            x.WorkoutSessionId == sessionId &&
            x.WorkoutSession.WorkoutPlan.AppUserId == userId);

            if(existentWorkoutSessionExercise == null)
            {
                return null;
            }

            existentWorkoutSessionExercise.ActualSets = WorkoutSessionExerciseDto.ActualSets;
            existentWorkoutSessionExercise.ActualReps = WorkoutSessionExerciseDto.ActualReps;
            existentWorkoutSessionExercise.ActualWeight = WorkoutSessionExerciseDto.ActualWeight;
            existentWorkoutSessionExercise.Notes = WorkoutSessionExerciseDto.Notes;
            existentWorkoutSessionExercise.IsCompleted = WorkoutSessionExerciseDto.IsCompleted;

            await _context.SaveChangesAsync();

            return existentWorkoutSessionExercise;
        }
    }
}