using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker.Models;

namespace Workout_Tracker.Inerface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}