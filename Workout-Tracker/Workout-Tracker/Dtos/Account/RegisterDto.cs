using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name must be 3 characters")]
        [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(12, ErrorMessage = "Password must be 12 characters")]
        [MaxLength(100, ErrorMessage = "Password cannot be over 100 characters")]
        public string? Password { get; set; }
    }
}