using LiftControlSystem.Domain.Enums;
using LiftControlSystem.Domain.Logic.LiftStrategy;

namespace LiftControlSystem.Domain.Logic.Managers
{
    public class SystemModeManager
    {
        private readonly Dictionary<LiftStrategies, ILiftSelectionStrategy> _strategies;
        private ILiftSelectionStrategy _currentStrategy;

        public SystemModeManager()
        {
            _strategies = new()
            {
                { LiftStrategies.Closest, new ClosestLiftStrategy() },
                { LiftStrategies.EnergySaving, new EnergySavingStrategy() }
            };
            _currentStrategy = _strategies[LiftStrategies.Closest]; // Set default to Closest
        }

        public ILiftSelectionStrategy CurrentStrategy => _currentStrategy;

        public bool TrySetMode(LiftStrategies mode)
        {
            if (_strategies.TryGetValue(mode, out var strategy))
            {
                _currentStrategy = strategy;
                return true;
            }
            return false;
        }
    }
}
