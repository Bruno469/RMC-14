using System;

[RegisterComponent]
public sealed class VehicleComponent : Component
{
    [DataField("fuelCapacity")]
    public float FuelCapacity { get; set; } = 100f;
    public float CurrentFuel { get; private set; }

    [DataField("fuelConsumptionRate")]
    public float FuelConsumptionRate { get; set; } = 0.1f;
    [DataField("engineIntegrity")]
    public float EngineIntegrity { get; set; } = 100f;
    public bool indestructible { get; set; }

    /// <summary>
    /// Used in window title and context menu
    /// </summary>
    [DataField("verb")]
    [ViewVariables(VVAccess.ReadOnly)]
    public string Verb = "enter-verb";
    /// <summary>
    /// Context menu image
    /// </summary>
    [DataField("verbImage")]
    [ViewVariables(VVAccess.ReadOnly)]
    public SpriteSpecifier? VerbImage = new SpriteSpecifier.Texture(new ("/Textures/Interface/VerbIcons/close.svg.192dpi.png"));

    public bool HasFuel() => CurrentFuel > 0;
}
