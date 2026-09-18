using Microsoft.EntityFrameworkCore;
using Workout_Tracker.Data;
using Workout_Tracker.Models;
using Workout_Tracker.Mappers;
using Workout_Tracker.Enums;
using Workout_Tracker.Dtos.WorkoutExercise;
using Workout_Tracker.Repository;

namespace Workout_Tracker.Tests;

public class WorkoutTrackerTests
{
    private static ApplicationDBContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDBContext(options);
    }

    [Fact]
    public void WorkoutPlanMapper_ShouldMapCorrectly()
    {
        var model = new WorkoutPlan
        {
            Id = 1,
            Title = "Chest Workout",
            ScheduledDate = new DateTime(2026, 9, 15),
            Status = WorkoutStatus.Pending
        };

        var result = model.ToWorkoutPlanDto();

        Assert.Equal(model.Id, result.Id);
        Assert.Equal(model.Title, result.Title);
        Assert.Equal(model.ScheduledDate, result.ScheduledDate);
        Assert.Equal(model.Status, result.Status);
    }

    [Fact]
    public void WorkoutExerciseMapper_ShouldMapCorrectly()
    {
        var model = new WorkoutExercise
        {
            Id = 1,
            Sets = 4,
            Reps = 10,
            Weight = 50,
            Notes = "Heavy"
        };

        var result = model.ToWorkoutExerciseDto();

        Assert.Equal(1, result.Id);
        Assert.Equal(4, result.Sets);
        Assert.Equal(10, result.Reps);
        Assert.Equal(50, result.Weight);
        Assert.Equal("Heavy", result.Notes);
    }

    [Fact]
    public void WorkoutExerciseCreateMapper_ShouldSetPlanIdAndExerciseId()
    {
        var dto = new CreateWorkoutExerciseRequestDto
        {
            ExerciseId = 5,
            Sets = 3,
            Reps = 12,
            Weight = 40,
            Notes = "Test"
        };

        var result = dto.ToWorkoutExerciseFromCreateDto(10);

        Assert.Equal(10, result.WorkoutPlanId);
        Assert.Equal(5, result.ExerciseId);
        Assert.Equal(3, result.Sets);
        Assert.Equal(12, result.Reps);
        Assert.Equal(40, result.Weight);
        Assert.Equal("Test", result.Notes);
    }

    [Fact]
    public async Task WorkoutExercise_Create_ShouldOnlyAllowOwnedPlan()
    {
        await using var context = CreateContext();

        context.WorkoutPlans.Add(new WorkoutPlan
        {
            Id = 1,
            AppUserId = "user1",
            Title = "My Plan"
        });

        await context.SaveChangesAsync();

        var repo = new WorkoutExerciseRepository(context);

        var model = new WorkoutExercise
        {
            WorkoutPlanId = 1,
            ExerciseId = 1,
            Sets = 3,
            Reps = 10
        };

        var result = await repo.CreateAsync(model, "user2");

        Assert.Null(result);
    }

    [Fact]
    public async Task WorkoutExercise_Create_ShouldWorkForOwner()
    {
        await using var context = CreateContext();

        context.WorkoutPlans.Add(new WorkoutPlan
        {
            Id = 1,
            AppUserId = "user1",
            Title = "My Plan"
        });

        context.Exercises.Add(new Exercise
        {
            Id = 1,
            Name = "Bench Press"
        });

        await context.SaveChangesAsync();

        var repo = new WorkoutExerciseRepository(context);

        var model = new WorkoutExercise
        {
            WorkoutPlanId = 1,
            ExerciseId = 1,
            Sets = 3,
            Reps = 10
        };

        var result = await repo.CreateAsync(model, "user1");

        Assert.NotNull(result);
        Assert.Equal(1, result!.WorkoutPlanId);
    }

    [Fact]
    public async Task WorkoutExercise_Update_ShouldNotAllowOtherUser()
    {
        await using var context = CreateContext();

        context.WorkoutPlans.Add(new WorkoutPlan
        {
            Id = 1,
            AppUserId = "user1",
            Title = "My Plan"
        });

        context.WorkoutExercises.Add(new WorkoutExercise
        {
            Id = 1,
            WorkoutPlanId = 1,
            ExerciseId = 1,
            Sets = 3,
            Reps = 10
        });

        await context.SaveChangesAsync();

        var repo = new WorkoutExerciseRepository(context);

        var dto = new UpdateWorkoutExerciseRequestDto
        {
            Sets = 5,
            Reps = 5,
            Weight = 100
        };

        var result = await repo.UpdateAsync(1, 1, dto, "user2");

        Assert.Null(result);
    }

    [Fact]
    public async Task WorkoutExercise_Delete_ShouldNotAllowOtherUser()
    {
        await using var context = CreateContext();

        context.WorkoutPlans.Add(new WorkoutPlan
        {
            Id = 1,
            AppUserId = "user1",
            Title = "My Plan"
        });

        context.WorkoutExercises.Add(new WorkoutExercise
        {
            Id = 1,
            WorkoutPlanId = 1,
            ExerciseId = 1,
            Sets = 3,
            Reps = 10
        });

        await context.SaveChangesAsync();

        var repo = new WorkoutExerciseRepository(context);

        var result = await repo.DeleteAsync(1, 1, "user2");

        Assert.Null(result);
    }

    [Fact]
    public async Task WorkoutExercise_GetAll_ShouldReturnOnlyOwnedPlanExercises()
    {
        await using var context = CreateContext();

        context.WorkoutPlans.AddRange(
            new WorkoutPlan
            {
                Id = 1,
                AppUserId = "user1",
                Title = "Plan 1"
            },
            new WorkoutPlan
            {
                Id = 2,
                AppUserId = "user2",
                Title = "Plan 2"
            });

        context.WorkoutExercises.AddRange(
            new WorkoutExercise
            {
                Id = 1,
                WorkoutPlanId = 1,
                ExerciseId = 1
            },
            new WorkoutExercise
            {
                Id = 2,
                WorkoutPlanId = 2,
                ExerciseId = 1
            });

        await context.SaveChangesAsync();

        var repo = new WorkoutExerciseRepository(context);

        var result = await repo.GetAllAsync(1, "user1");

        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
    }

    [Fact]
    public async Task WorkoutExercise_GetAll_ShouldReturnEmptyForOtherUser()
    {
        await using var context = CreateContext();

        context.WorkoutPlans.Add(new WorkoutPlan
        {
            Id = 1,
            AppUserId = "user1",
            Title = "Plan"
        });

        context.WorkoutExercises.Add(new WorkoutExercise
        {
            Id = 1,
            WorkoutPlanId = 1,
            ExerciseId = 1
        });

        await context.SaveChangesAsync();

        var repo = new WorkoutExerciseRepository(context);

        var result = await repo.GetAllAsync(1, "user2");

        Assert.Empty(result);
    }
}