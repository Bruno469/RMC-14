using System.Numerics;
using Content.Shared.Curse;
using Content.Server.Bible.Components;
using Content.Server.Popups;
using Content.Shared.Database;
using Content.Shared.Popups;
using Content.Shared.Verbs;
using Content.Shared.Actions;
using Robust.Server.GameObjects;
using Robust.Shared.Player;
using Robust.Shared.Map;
using Robust.Shared.Random;
using Robust.Shared.Map.Components;

namespace Content.Server._RMC14.Vehicle
{
    public sealed class VehicleSystem : EntitySystem
    {
        [Dependency] private readonly PopupSystem _popupSystem = default!;
        [Dependency] private readonly IMapManager _mapManager = default!;
        [Dependency] private readonly MapLoaderSystem _map = default!;
        [Dependency] private readonly SharedActionsSystem _actionSystem = default!;
        [Dependency] private readonly MetaDataSystem _metaDataSystem = default!;
        [Dependency] private readonly SharedTransformSystem _transform = default!;
        [Dependency] private readonly IRobustRandom _random = default!;

        public EntityUid VehicleInteriorMap { get; private set; } = new();
        public EntityUid? VehicleInteriorGrid { get; private set; } = new();

        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<VehicleEngineComponent, ComponentInit>(OnComponentInit);
            SubscribeLocalEvent<VehicleEngineComponent, GetVerbsEvent<ActivationVerb>>(Tryteleport);
        }

        private void OnComponentInit(EntityUid uid, VehicleEngineComponent comp, ComponentInit args)
        {
            var (mapUid, gridUid) = LoadInterior(uid, comp);
        }

        private void Tryteleport(EntityUid uid, VehicleEngineComponent comp, GetVerbsEvent<ActivationVerb> args)
        {
            // if it doesn't have an actor and we can't reach it then don't add the verb
            if (!EntityManager.TryGetComponent(args.User, out ActorComponent? actor))
                return;

            // this is to prevent ghosts from using it
            if (!args.CanInteract)
                return;

            var enterVerb = new ActivationVerb
            {
                Text = Loc.GetString(comp.Verb),
                Icon = comp.VerbImage,
                Act = () =>
                {
                    if (true) //conditions for entering the vehicle
                    {
                        _popupSystem.PopupEntity(Loc.GetString("popup-notify-fail-try"), uid, actor.PlayerSession, PopupType.Large);
                        return;
                    }
                    _popupSystem.PopupEntity(Loc.GetString("popup-notify-enter"), uid, actor.PlayerSession, PopupType.Large);
                    _actionSystem.StartUseDelay(args.User);
                    _transform.SetCoordinates(args.User, new EntityCoordinates(gridUid ?? mapUid, Vector2.One));
                },
                Impact = LogImpact.Low,
            };

            args.Verbs.Add(enterVerb);
        }

        public (EntityUid Map, EntityUid? Grid) LoadInterior(EntityUid uid, VehicleEngineComponent comp)
        {
            VehicleInteriorMap = _mapManager.GetMapEntityId(_mapManager.CreateMap());
            _metaDataSystem.SetEntityName(VehicleInteriorMap, $"M577-{VehicleInteriorMap}");
            var VehicleInteriorMapId = Comp<MapComponent>(VehicleInteriorMap).MapId;

            var grids = _map.LoadMap(VehicleInteriorMapId, // path to interior of vehicle ex:."/Maps/vehicles/apc.yml");

            if (grids.Count != 0)
            {
                _metaDataSystem.SetEntityName(grids[0], $"M577-{VehicleInteriorMap}");
                VehicleInteriorGrid = grids[0];
            }
            else
            {
                VehicleInteriorGrid = null;
            }

            _mapManager.SetMapPaused(VehicleInteriorMapId, false);
            return (VehicleInteriorMap, VehicleInteriorGrid);
        }
    }
}
