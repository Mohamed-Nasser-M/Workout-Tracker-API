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
    [Route("api/exercises")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepo;
        public ExerciseController(IExerciseRepository exerciseRepo)
        {
            _exerciseRepo = exerciseRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ExerciseQueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var exercises = await _exerciseRepo.GetAllAsync(query);
            var exerciseDto = exercises.Select(s => s.ToExerciseDto()).ToList();

            return Ok(exerciseDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var exercise = await _exerciseRepo.GetByIdAsync(id);

            if(exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise.ToExerciseDto());
        }
    }
}