using System.Runtime.CompilerServices;
using Robust.Shared.Network;
using Robust.Shared.Physics.Events;
using Robust.Shared.Audio;
using Robust.Shared.Audio.System;
using Robust.Shared.Physics.Component;
using Robust.Shared.Physics.System;
using Robust.Shared.Random;
using System.IO;

namespace Content.Shared._RMC14.Projectiles;

public sealed class CMFlakProjectileSystem : EntitySystem
{
    [Dependency] private readonly INetManager _net = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedPhysicsSystem _physics = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<FlakProjectileComponent, StartCollideEvent>(OnCollideFlak);
    }

    private void OnCollideFlak(Entity<FlakProjectileComponent> ent, ref StartCollideEvent args)
    {
        _audio.PlayPvs();
        DirectionToFlak = args.Disrection;
        TypeOfFlaks = RobustRandom.Pick(ent.Flaks);
        var flaks = Spawn(TypeOfFlaks, DirectionToFlak);
        var physics = Comp<PhysicsComponent>(flaks);
        _physics.ApplyLinearImpulse(flaks, -offset.Normalized() * 2, body: physics);
        if (_net.IsServer)
            QueueDel(ent);
    }
}
