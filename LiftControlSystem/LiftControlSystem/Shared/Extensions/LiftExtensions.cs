using LiftControlSystem.Domain.Models;
using LiftControlSystem.DTOs;

namespace LiftControlSystem.Shared.Extensions
{
    public static class LiftExtensions
    {
        public static LiftDto? ToDto(this Lift lift)
        {
            if (lift is null)
                return null;

            return new LiftDto(
                Id: lift.Id,
                CurrentFloor: lift.CurrentFloor,
                State: lift.State,
                ServiceableFloors: lift.ServiceableFloors);
        }
    }
}
