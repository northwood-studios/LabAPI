using Generators;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="RoomLightController">room light controller</see>.
/// </summary>
public class LightsController
{
    /// <summary>
    /// A reference to all <see cref="LightsController"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<LightsController> List => Dictionary.Values;

    /// <summary>
    /// Contains all the cached rooms in the game, accessible through their <see cref="RoomLightController"/>.
    /// </summary>
    private static Dictionary<RoomLightController, LightsController> Dictionary { get; } = [];

    /// <summary>
    /// Gets the controller wrapper from <see cref="Dictionary"/>, or creates a new one if it doesn't exists.
    /// </summary>
    /// <param name="roomLightController">The original light controller.</param>
    /// <returns>The requested light controller wrapper.</returns>
    [return: NotNullIfNotNull(nameof(roomLightController))]
    public static LightsController? Get(RoomLightController roomLightController)
    {
        if (roomLightController == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(roomLightController, out LightsController lightController) ? lightController : new LightsController(roomLightController);
    }

    /// <summary>
    /// Initializes the Room wrapper by subscribing to the <see cref="RoomLightController"/> events.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        RoomLightController.OnAdded += (roomLightController) => _ = new LightsController(roomLightController);
        RoomLightController.OnRemoved += (roomLightController) => Dictionary.Remove(roomLightController);
    }

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="original">The original object.</param>
    private LightsController(RoomLightController original)
    {
        Dictionary.Add(original, this);
        Base = original;
    }

    /// <summary>
    /// The base game object.
    /// </summary>
    public RoomLightController Base { get; }

    /// <summary>
    /// The room this controller is assigned to.
    /// </summary>
    public Room Room => Room.Get(Base.Room)!;

    /// <summary>
    /// Gets or sets whether the lights are enabled in this room.
    /// </summary>
    public bool LightsEnabled
    {
        get => Base.NetworkLightsEnabled;
        set => Base.NetworkLightsEnabled = value;
    }

    /// <summary>
    /// Gets or sets the overriden room light color. Set the value to <see cref="Color.clear"/> to reset override color.
    /// </summary>
    public Color OverrideLightsColor
    {
        get => Base.NetworkOverrideColor;
        set => Base.NetworkOverrideColor = value;
    }

    /// <summary>
    /// Blackouts the room for specified duration.
    /// </summary>
    /// <param name="duration">Duration of light shutdown in seconds.</param>
    public void FlickerLights(float duration) => Base.ServerFlickerLights(duration);
}