using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout_Tracker.Data;
using Workout_Tracker.Helpers;
using Workout_Tracker.Inerface;
using Workout_Tracker.Mappers;

namespace Workout_Tracker.Controllers
{
    [Authorize]
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        
        private readonly IReportsRepository _reportsRepo;
        public ReportsController(IReportsRepository reportsRepo)
        {
            _reportsRepo = reportsRepo;
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetWorkoutHistory()
        {
            var userId = User.GetUserId();

            var workoutHistory = await _reportsRepo.GetWorkoutHistoryAsync(userId);
            var workoutHistoryDto = workoutHistory.Select(w => w.ToWorkoutHistoryDto()).ToList();

            return Ok(workoutHistoryDto);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetWorkoutSummary()
        {
            var userId = User.GetUserId();

            var workoutSummary = await _reportsRepo.GetWorkoutSummaryAsync(userId);
            
            return Ok(workoutSummary);
        }

        [HttpGet("progress")]
        public async Task<IActionResult> GetWorkoutProgress()
        {
            var userId = User.GetUserId();

            var WorkoutProgress = await _reportsRepo.GetExerciseProgressAsync(userId);

            return Ok(WorkoutProgress);
        }
    }
}