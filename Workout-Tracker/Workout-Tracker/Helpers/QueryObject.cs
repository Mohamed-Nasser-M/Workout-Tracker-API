using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Enums;

namespace Workout_Tracker.Helpers
{
    public class QueryObject
    {
        public WorkoutStatus? Status { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
    }
}