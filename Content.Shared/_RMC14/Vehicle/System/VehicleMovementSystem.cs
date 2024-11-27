using System.Numerics;
using Content.Shared.Drunk;
using Content.Shared.StatusEffect;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Enums;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
public sealed class VehicleMovementSystem : EntitySystem
{
    [Dependency] private readonly IEntityManager _entityManager = default!;
    public void Move(EntityUid user, Vector2 direction)
    {
        if(!CanMove()) return;

        var transform = _entityManager.GetComponent<TransformComponent>(Owner);
        transform += direction * Speed * IoCManager.Resolve<IGameTiming>().FrameTime;

        IsMoving = true;
    }
    public void Stop()
    {
        IsMoving = false;
    }
    private bool CanMove()
    {
        if (!_entityManager.TryGetComponent<VehicleComponent(Owner, out var enginer)) return false;
        if (enginer.EngineIntegrity <= 0 && !enginer.indestructible)
            return false;
        return enginer.HasFuel();
    }
}
