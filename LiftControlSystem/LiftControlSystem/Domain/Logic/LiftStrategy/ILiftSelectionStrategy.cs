using LiftControlSystem.Domain.Models;

namespace LiftControlSystem.Domain.Logic.LiftStrategy
{
    public interface ILiftSelectionStrategy
    {
        Lift? SelectLift(IEnumerable<Lift> lifts, int requestedFloor);
    }
}
