using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker.Data;
using Workout_Tracker.Dtos.WorkoutExercise;
using Workout_Tracker.Inerface;
using Workout_Tracker.Models;

namespace Workout_Tracker.Repository
{
    public class WorkoutExerciseRepository : IWorkoutExerciseRepository
    {
        public readonly ApplicationDBContext _context;
        public WorkoutExerciseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<WorkoutExercise?> CreateAsync(WorkoutExercise workoutExerciseModel, string userId)
        {
            var workoutPlan = await _context.WorkoutPlans
                .FirstOrDefaultAsync(w =>
                    w.Id == workoutExerciseModel.WorkoutPlanId &&
                    w.AppUserId == userId);

            if (workoutPlan == null)
                return null;
            
            var Exercise = await _context.Exercises
                .FirstOrDefaultAsync(e =>
                    e.Id == workoutExerciseModel.ExerciseId);
            
            if (Exercise == null)
                return null;

            await _context.WorkoutExercises.AddAsync(workoutExerciseModel);
            await _context.SaveChangesAsync();

            return workoutExerciseModel;
        }

        public async Task<WorkoutExercise?> DeleteAsync(int workoutPlanId, int id, string userId)
        {
            var workoutExerciseModel = await _context.WorkoutExercises.FirstOrDefaultAsync(w => 
            w.Id == id && 
            w.WorkoutPlanId == workoutPlanId &&
            w.WorkoutPlan.AppUserId == userId);

            if(workoutExerciseModel == null)
            {
                return null;
            }

            _context.WorkoutExercises.Remove(workoutExerciseModel);
            await _context.SaveChangesAsync();
            return workoutExerciseModel;
        }

        public async Task<List<WorkoutExercise>> GetAllAsync(int workoutPlanId, string userId)
        {
            return await _context.WorkoutExercises.Where(w => w.WorkoutPlanId == workoutPlanId && w.WorkoutPlan.AppUserId == userId).ToListAsync();
        }

        public async Task<WorkoutExercise?> UpdateAsync(int workoutPlanId, int id, UpdateWorkoutExerciseRequestDto WorkoutExerciseDto, string userId)
        {
            var existentWorkoutExercise = await _context.WorkoutExercises
                .FirstOrDefaultAsync(w =>
                    w.WorkoutPlan.Id == workoutPlanId &&
                    w.Id == id &&
                    w.WorkoutPlan.AppUserId == userId);
            
            if(existentWorkoutExercise == null)
            {
                return null;
            }

            existentWorkoutExercise.Sets = WorkoutExerciseDto.Sets;
            existentWorkoutExercise.Reps = WorkoutExerciseDto.Reps;
            existentWorkoutExercise.Weight = WorkoutExerciseDto.Weight;
            existentWorkoutExercise.Notes = WorkoutExerciseDto.Notes;

            await _context.SaveChangesAsync();

            return existentWorkoutExercise;
        }
    }
}