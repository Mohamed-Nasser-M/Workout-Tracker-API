using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout_Tracker.Dtos;
using Workout_Tracker.Dtos.WorkoutSessionExercise;
using Workout_Tracker.Helpers;
using Workout_Tracker.Inerface;
using Workout_Tracker.Mappers;

namespace Workout_Tracker.Controllers
{
    [Authorize]
    [Route("api/sessions/{sessionId}/exercises")]
    [ApiController]
    public class WorkoutSessionExerciseController : ControllerBase
    {
        private readonly IWorkoutSessionExerciseRepository _workoutsessionexerciseRepo;
        public WorkoutSessionExerciseController(IWorkoutSessionExerciseRepository WorkoutSessionExerciseRepo)
        {
            _workoutsessionexerciseRepo = WorkoutSessionExerciseRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] int sessionId)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var workoutsessionexercises = await _workoutsessionexerciseRepo.GetAllAsync(sessionId, userId);
            var workoutsessionexerciseDto = workoutsessionexercises.Select(s => s.ToWorkoutSessionExerciseDto()).ToList();

            return Ok(workoutsessionexerciseDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int sessionId,[FromRoute] int id, [FromBody] UpdateWorkoutSessionExerciseRequestDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var workoutsessionexerciseModel = await _workoutsessionexerciseRepo.UpdateAsync(sessionId, id, updateDto, userId);

            if(workoutsessionexerciseModel == null)
            {
                return NotFound();
            }

            return Ok(workoutsessionexerciseModel.ToWorkoutSessionExerciseDto());
        }
    }
}