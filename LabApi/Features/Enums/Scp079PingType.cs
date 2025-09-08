using LabApi.Features.Wrappers;

namespace LabApi.Features.Enums;

/// <summary>
/// Enum used for type of the ping.
/// </summary>
public enum Scp079PingType : byte
{
    /// <summary>
    /// <see cref="Wrappers.Generator"/> ping.
    /// </summary>
    Generator = 0,

    /// <summary>
    /// <see cref="ExplosiveGrenadeProjectile"/> and <see cref="FlashbangProjectile"/> pings.
    /// </summary>
    Projectile = 1,

    /// <summary>
    /// Micro-HID ping.
    /// </summary>
    MicroHid = 2,

    /// <summary>
    /// <see cref="Player"/> human role ping.
    /// </summary>
    Human = 3,

    /// <summary>
    /// <see cref="Wrappers.Elevator"/> ping.
    /// </summary>
    Elevator = 4,

    /// <summary>
    /// <see cref="Wrappers.Door"/> ping.
    /// </summary>
    Door = 5,

    /// <summary>
    /// Default "i" icon ping.
    /// </summary>
    Default = 6,
}
