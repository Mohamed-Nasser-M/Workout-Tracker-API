using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Workout_Tracker.Data;
using Workout_Tracker.Dtos;
using Workout_Tracker.Helpers;
using Workout_Tracker.Inerface;
using Workout_Tracker.Mappers;
using Workout_Tracker.Models;

namespace Workout_Tracker.Controllers
{
    [Authorize]
    [Route("api/workoutplans")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanRepository _workoutplanRepo;
        public WorkoutPlanController(IWorkoutPlanRepository workoutplanRepo)
        {
            _workoutplanRepo = workoutplanRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var workoutplans = await _workoutplanRepo.GetAllAsync(userId, query);
            var workoutplanDto = workoutplans.Select(s => s.ToWorkoutPlanDto()).ToList();

            return Ok(workoutplanDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var workoutplan = await _workoutplanRepo.GetByIdAsync(id, userId);

            if(workoutplan == null)
            {
                return NotFound();
            }

            return Ok(workoutplan.ToWorkoutPlanDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkoutPlanRequestDto workoutPlanDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();
            
            var workoutplanModel = workoutPlanDto.ToWorkoutPlanFromCreateDto(userId);
            await _workoutplanRepo.CreateAsync(workoutplanModel);
            return CreatedAtAction(nameof(GetById), new {id = workoutplanModel.Id}, workoutplanModel.ToWorkoutPlanDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWorkoutPlanRequestDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var workoutplanModel = await _workoutplanRepo.UpdateAsync(id, updateDto, userId);

            if(workoutplanModel == null)
            {
                return NotFound();
            }

            return Ok(workoutplanModel.ToWorkoutPlanDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();
            
            var workoutplanModel = await _workoutplanRepo.DeleteAsync(id, userId);

            if(workoutplanModel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}