using System.Numerics;
using Content.Shared.Drunk;
using Content.Shared.StatusEffect;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Enums;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
public sealed class VehicleControlSystem : EntitySystem
{
    [Dependency] private readonly IEntityManager _entityManager = default!;
    public void EnterVehicle(EntityUid user)
    {
        if (Driver != null) return;

        Driver = user;
        _entityManager.GetComponent<transformComponent>(user).AttachParent(Owner);
    }
    public void ExitVehicle(EntityUid user)
    {
        if (Driver != user) return;
        Driver = null;

        _entityManager.GetComponent<transformComponent>(user).AttachParent(null);
    }
}
