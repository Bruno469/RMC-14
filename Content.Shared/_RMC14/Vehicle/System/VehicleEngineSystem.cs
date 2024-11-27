using System.Numerics;
using Content.Shared.Drunk;
using Content.Shared.StatusEffect;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Enums;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
public sealed class VehicleEngineSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        CurrentFuel = FuelCapacity;
    }
    public void ConsumeFuel(float deltaTime)
    {
        if (CurrentFuel <= 0)
            return;
        CurrentFuel -= FuelConsumptionRate * deltaTime;
        if (CurrentFuel < 0)
            CurrentFuel = 0;
    }
    public void Refuel(float amount)
    {
        CurrentFuel = MathF.Min(FuelCapacity, CurrentFuel + amount);
    }
}
