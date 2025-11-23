using LiftControlSystem.Domain.Enums;

namespace LiftControlSystem.DTOs
{
    public record LiftDto(int Id, int CurrentFloor, LiftState State, IReadOnlyList<int> ServiceableFloors);
}
