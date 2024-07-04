using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Robust.Shared.GameStates;

namespace Content.Shared._RMC14.Projectiles;

[RegisterComponent, NetworkedComponent]
[Access(typeof(CMFlakProjectileSystem))]
public sealed partial class FlakProjectileComponent : Component
{
    [DataField]
    public Dictionary<EntProtoId, float> Flaks = new();
}
