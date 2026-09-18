using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout_Tracker.Dtos.WorkoutExercise;
using Workout_Tracker.Helpers;
using Workout_Tracker.Inerface;
using Workout_Tracker.Mappers;

namespace Workout_Tracker.Controllers
{
    [Authorize]
    [Route("api/workoutplans/{workoutPlanId}/exercises")]
    [ApiController]
    public class WorkoutExerciseController : ControllerBase
    {
        private readonly IWorkoutExerciseRepository _workoutExerciseRepo;
        public WorkoutExerciseController(IWorkoutExerciseRepository workoutExerciseRepo)
        {
            _workoutExerciseRepo = workoutExerciseRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] int workoutPlanId)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var workoutExercises = await _workoutExerciseRepo.GetAllAsync(workoutPlanId, userId);
            var workoutExerciseDto = workoutExercises.Select(s => s.ToWorkoutExerciseDto()).ToList();

            return Ok(workoutExerciseDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int workoutPlanId, [FromBody] CreateWorkoutExerciseRequestDto workoutExerciseDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();
            
            var workoutexerciseModel = workoutExerciseDto.ToWorkoutExerciseFromCreateDto(workoutPlanId);

            var result = await _workoutExerciseRepo.CreateAsync(workoutexerciseModel, userId);

            if (result == null)
                return BadRequest();
            
            return CreatedAtAction(nameof(GetAll), new { workoutPlanId }, workoutexerciseModel.ToWorkoutExerciseDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int workoutPlanId, [FromRoute] int id, [FromBody] UpdateWorkoutExerciseRequestDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var workoutexerciseModel = await _workoutExerciseRepo.UpdateAsync(workoutPlanId, id, updateDto, userId);

            if(workoutexerciseModel == null)
            {
                return NotFound();
            }

            return Ok(workoutexerciseModel.ToWorkoutExerciseDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int workoutPlanId, [FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();
            
            var workoutexerciseModel = await _workoutExerciseRepo.DeleteAsync(workoutPlanId, id, userId);

            if(workoutexerciseModel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}