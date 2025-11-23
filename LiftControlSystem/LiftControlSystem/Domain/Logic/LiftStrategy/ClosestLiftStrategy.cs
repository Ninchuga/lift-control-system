using LiftControlSystem.Domain.Models;
using LiftControlSystem.Domain.Enums;

namespace LiftControlSystem.Domain.Logic.LiftStrategy
{
    public class ClosestLiftStrategy : ILiftSelectionStrategy
    {
        public Lift? SelectLift(IEnumerable<Lift> lifts, int requestedFloor) =>
            lifts
            .Where(l => l.CanServe(requestedFloor))
            .OrderBy(l => l.State == LiftState.Idle ? 0 : 1)
            .ThenBy(l => Math.Abs(l.CurrentFloor - requestedFloor))
            .FirstOrDefault();
    }
}
