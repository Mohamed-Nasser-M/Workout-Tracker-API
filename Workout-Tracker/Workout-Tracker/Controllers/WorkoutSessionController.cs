using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout_Tracker.Data;
using Workout_Tracker.Dtos;
using Workout_Tracker.Helpers;
using Workout_Tracker.Inerface;
using Workout_Tracker.Mappers;

namespace Workout_Tracker.Controllers
{
    [Authorize]
    [Route("api/sessions")]
    [ApiController]
    public class WorkoutSessionController : ControllerBase
    {
        private readonly IWorkoutSessionRepository _workoutsessionRepo;
        public WorkoutSessionController(IWorkoutSessionRepository WorkoutSessionRepo)
        {
            _workoutsessionRepo = WorkoutSessionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();
            
            var workoutsessions = await _workoutsessionRepo.GetAllAsync(userId);
            var workoutsessionDto = workoutsessions.Select(s => s.ToWorkoutSessionDto()).ToList();

            return Ok(workoutsessionDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();
            
            var workoutsession = await _workoutsessionRepo.GetByIdAsync(id, userId);

            if(workoutsession == null)
            {
                return NotFound();
            }

            return Ok(workoutsession.ToWorkoutSessionDto());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkoutSessionRequestDto WorkoutSessioDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var workoutsessionModel = WorkoutSessioDto.ToWorkoutSessionFromCreateDto();

            var createdWorkoutSession = await _workoutsessionRepo.CreateAsync(workoutsessionModel, userId);
            if (createdWorkoutSession == null)
            {
                return NotFound("Workout plan not found or does not belong to the user.");
            }

            return CreatedAtAction(nameof(GetById), new {id = workoutsessionModel.Id}, workoutsessionModel.ToWorkoutSessionDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWorkoutSessionRequestDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();
            
            var workoutsessionModel = await _workoutsessionRepo.UpdateAsync(id, updateDto, userId);

            if(workoutsessionModel == null)
            {
                return NotFound();
            }

            return Ok(workoutsessionModel.ToWorkoutSessionDto());
        }
    }
}