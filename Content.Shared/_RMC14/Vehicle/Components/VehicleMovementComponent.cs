using System.Numerics;

[RegisterComponent]
public sealed class VehicleMovementComponent : Component
{
    [DataField("speed")]
    public float Speed { get; set; } = 5.0f;
    [DataField("turnRate")]
    public float TurnRate { get; set; } = 90.0f;
    public bool IsMoving { get; private set;}
}
