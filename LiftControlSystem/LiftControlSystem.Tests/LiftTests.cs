using FluentAssertions;
using LiftControlSystem.Domain.Enums;
using LiftControlSystem.Domain.Models;

namespace LiftControlSystem.Tests
{
    public class LiftTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(10)]
        public void GivenTenLiftFloors_WhenSupportedFloorIsSelected_ThenLiftShouldServeThatFloor(int floor)
        {
            var lift = new Lift(
                id: 1,
                currentFloor: 0,
                state: LiftState.Idle,
                serviceableFloors: [..Enumerable.Range(1, 10)]
            );

            bool canServe = lift.CanServe(floor);

            canServe.Should().BeTrue();
        }

        [Theory]
        [InlineData(15)]
        [InlineData(11)]
        [InlineData(19)]
        public void GivenTenLiftFloors_WhenNonSupportedFloorIsSelected_ThenLiftShouldNotServeThatFloor(int floor)
        {
            var lift = new Lift(
                id: 1,
                currentFloor: 0,
                state: LiftState.Idle,
                serviceableFloors: [.. Enumerable.Range(1, 10)]
            );

            bool canServe = lift.CanServe(floor);

            canServe.Should().BeFalse();
        }

        [Theory]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(10)]
        public async Task GivenTenLiftFloors_WhenSupportedFloorIsSelected_ThenLiftShouldMoveToThatFloor(int floor)
        {
            var lift = new Lift(
                id: 1,
                currentFloor: 0,
                state: LiftState.Idle,
                serviceableFloors: [.. Enumerable.Range(1, 10)]
            );

            var liftResult = await lift.MoveToFloor(floor);

            liftResult.Success.Should().BeTrue();
            lift.CurrentFloor.Should().Be(floor);
        }

        [Theory]
        [InlineData(15)]
        [InlineData(11)]
        [InlineData(19)]
        public async Task GivenTenLiftFloors_WhenNotSupportedFloorIsSelected_ThenLiftShouldNotMoveToThatFloor(int floor)
        {
            var lift = new Lift(
                id: 1,
                currentFloor: 0,
                state: LiftState.Idle,
                serviceableFloors: [.. Enumerable.Range(1, 10)]
            );

            var liftResult = await lift.MoveToFloor(floor);

            liftResult.Warnings.Should().ContainSingle()
                .Which.Should().Be($"Lift {lift.Id} cannot serve floor {floor}.");
        }
    }
}
