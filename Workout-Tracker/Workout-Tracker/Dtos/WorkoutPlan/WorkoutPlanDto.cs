using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;
using Workout_Tracker.Models;

namespace Workout_Tracker.Dtos
{
    public class WorkoutPlanDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        public WorkoutStatus Status { get; set; }
    }
}