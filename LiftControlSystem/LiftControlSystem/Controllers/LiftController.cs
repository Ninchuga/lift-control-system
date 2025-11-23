using LiftControlSystem.Domain.Logic.Managers;
using LiftControlSystem.Domain.Models;
using LiftControlSystem.Shared;

namespace LiftControlSystem.Controllers
{
    public class LiftController(SystemModeManager modeManager)
    {
        // TODO: Replace with actual data source or repository
        private readonly List<Lift> _lifts =
            [
                new Lift(id: 1, currentFloor: 0, state: Domain.Enums.LiftState.Idle, serviceableFloors: [.. Enumerable.Range(0, 10)]),
                new Lift(id: 2, currentFloor: 5, state: Domain.Enums.LiftState.Idle, serviceableFloors: [.. Enumerable.Range(5, 15)])
            ];

        private readonly SystemModeManager _modeManager = modeManager;

        public async Task<ResultData<Lift>> AssignLiftAsync(int requestedFloor)
        {
            var strategy = _modeManager.CurrentStrategy;
            var lift = strategy.SelectLift(_lifts, requestedFloor);
            if (lift is null)
                return new ResultData<Lift>().WithWarning($"No lifts available for requested floor '{requestedFloor}'.");

            return await lift.MoveToFloor(requestedFloor);
        }
    }
}
