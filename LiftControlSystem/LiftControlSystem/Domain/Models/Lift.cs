using LiftControlSystem.Domain.Enums;
using LiftControlSystem.Shared;

namespace LiftControlSystem.Domain.Models
{
    public record Lift
    {
        public Lift(int id, int currentFloor, LiftState state, IReadOnlyList<int> serviceableFloors)
        {
            Id = id;
            CurrentFloor = currentFloor;
            State = state;
            ServiceableFloors = serviceableFloors;
        }

        public int Id { get; init; }
        public int CurrentFloor { get; private set; }
        public LiftState State { get; private set; } = LiftState.Idle;
        public IReadOnlyList<int> ServiceableFloors { get; init; } = [];

        public bool CanServe(int floor) => ServiceableFloors.Contains(floor);

        public async Task<ResultData<Lift>> MoveToFloor(int targetFloor)
        {
            if (!CanServe(targetFloor))
                return new ResultData<Lift>().WithWarning($"Lift {Id} cannot serve floor {targetFloor}.");

            State = LiftState.Moving; // TODO: notify observers about state change -> publish domain event
            await Task.Delay(2000); // Simulated movement delay
            CurrentFloor = targetFloor;
            State = LiftState.Idle;

            return ResultData<Lift>.Ok(this);
        }
    }
}
