using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Helpers;
using Workout_Tracker.Models;

namespace Workout_Tracker.Inerface
{
    public interface IExerciseRepository
    {
        Task<Exercise?> GetByIdAsync(int id);
        Task<List<Exercise>> GetAllAsync(ExerciseQueryObject query);
    }
}