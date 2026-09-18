using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker.Data;
using Workout_Tracker.Helpers;
using Workout_Tracker.Inerface;
using Workout_Tracker.Models;

namespace Workout_Tracker.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDBContext _context;
        public ExerciseRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Exercise>> GetAllAsync(ExerciseQueryObject query)
        {
            var exercises = _context.Exercises.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Category.ToString()))
            {
                exercises = exercises.Where(s => s.Category.ToString().Contains(query.Category.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(query.MuscleGroup.ToString()))
            {
                exercises = exercises.Where(s => s.MuscleGroup.ToString().Contains(query.MuscleGroup.ToString()));
            }

            return await exercises.ToListAsync();
        }

        public async Task<Exercise?> GetByIdAsync(int id)
        {
            return await _context.Exercises.FindAsync(id);
        }
    }
}