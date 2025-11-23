using FluentAssertions;
using LiftControlSystem.Domain.Enums;
using LiftControlSystem.Domain.Logic.LiftStrategy;
using LiftControlSystem.Domain.Models;

namespace LiftControlSystem.Tests
{
    public class EnergySavingStrategyTests
    {
        private readonly EnergySavingStrategy _strategy = new();

        private Lift CreateLift(int id, int currentFloor, LiftState state, params int[] serviceableFloors)
            => new(id, currentFloor, state, serviceableFloors);

        [Fact]
        public void SelectLift_ReturnsNull_WhenNoLifts()
        {
            var lift = _strategy.SelectLift(lifts: [], 2);

            lift.Should().BeNull();
        }

        [Fact]
        public void SelectLift_ReturnsNull_WhenNoLiftCanServeRequestedFloor()
        {
            var lifts = new[]
            {
            CreateLift(1, 0, LiftState.Idle, 1, 3),
            CreateLift(2, 5, LiftState.Moving, 4, 5)
        };

            var lift = _strategy.SelectLift(lifts, 2);

            lift.Should().BeNull();
        }

        [Fact]
        public void SelectLift_ReturnsOnlyLiftThatCanServe()
        {
            var lift = CreateLift(1, 0, LiftState.Idle, 0, 1, 2, 3);

            var selectedLift = _strategy.SelectLift(new[] { lift }, 2);

            lift.Should().Be(selectedLift);
        }

        [Fact]
        public void SelectLift_PrefersIdleLift_WhenMultipleAtSameServiceableCount()
        {
            var idleLift = CreateLift(1, 2, LiftState.Idle, 1, 2, 3);
            var movingLift = CreateLift(2, 2, LiftState.Moving, 1, 2, 3);
            var lifts = new[] { movingLift, idleLift };

            var selectedLift = _strategy.SelectLift(lifts, 2);

            idleLift.Should().Be(selectedLift);
        }

        [Fact]
        public void SelectLift_PrefersLiftWithFewerServiceableFloors()
        {
            var lift1 = CreateLift(1, 1, LiftState.Idle, 1, 2, 3, 4, 5);
            var lift2 = CreateLift(2, 2, LiftState.Idle, 2, 3);
            var lifts = new[] { lift1, lift2 };

            var selectedLift = _strategy.SelectLift(lifts, 2);

            lift2.Should().Be(selectedLift);
        }

        [Fact]
        public void SelectLift_ReturnsFirstLift_WhenMultipleIdleWithSameServiceableCount()
        {
            var lift1 = CreateLift(1, 2, LiftState.Idle, 2, 3);
            var lift2 = CreateLift(2, 4, LiftState.Idle, 2, 3);
            var lifts = new[] { lift1, lift2 };

            var selectedLift = _strategy.SelectLift(lifts, 2);

            lift1.Should().Be(selectedLift);
        }
    }
}