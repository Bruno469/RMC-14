[RegisterComponent]
public sealed class VehicleControlComponent : Component
{
    private EntityUid? Driver;
    public bool IsControlled() => Driver != null;
    public EntityUid? GetDriver() => Driver;
}
