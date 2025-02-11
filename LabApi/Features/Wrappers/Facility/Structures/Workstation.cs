using InventorySystem.Items.Firearms.Attachments;
using MapGeneration.Distributors;
using System.Collections.Generic;
using System.Diagnostics;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper over the <see cref="WorkstationController"/> object.
/// </summary>
// TODO: add event to allow/deny attachment changes.
public class Workstation : Structure
{
    /// <summary>
    /// Contains all the cached workstations, accessible through their <see cref="SpawnableStructure"/>.
    /// </summary>
    public new static Dictionary<SpawnableStructure, Workstation> Dictionary = [];

    /// <summary>
    /// A reference to all <see cref="Workstation"/> instances.
    /// </summary>
    public new static IReadOnlyCollection<Workstation> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="spawnableStructure">The base <see cref="SpawnableStructure"/> object.</param>
    internal Workstation(SpawnableStructure spawnableStructure) : base(spawnableStructure)
    {
        BaseController = spawnableStructure.GetComponent<WorkstationController>();
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// Gets the base <see cref="WorkstationController"/> object.
    /// </summary>
    public WorkstationController BaseController { get; }

    /// <summary>
    /// Gets or sets the current <see cref="WorkstationController.WorkstationStatus"/> of the workstation.
    /// </summary>
    public WorkstationController.WorkstationStatus Status
    {
        get => (WorkstationController.WorkstationStatus)BaseController.Status;
        set => BaseController.NetworkStatus = (byte)value;
    }

    /// <summary>
    /// Gets the stopwatch used by the workstation for logic such as powering up/down, inactive time etc.
    /// </summary>
    public Stopwatch Stopwatch => BaseController.ServerStopwatch;

    /// <summary>
    /// Gets or sets the current user keeping the workstation on.
    /// </summary>
    public Player KnownUser
    {
        get => Player.Get(BaseController.KnownUser);
        set => BaseController.KnownUser = value.ReferenceHub;
    }

    /// <summary>
    /// Gets whether the specified <see cref="Player"/> is close enough to the workstation to keep it on.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to test.</param>
    /// <returns>Whether the player was close enough.</returns>
    public bool IsInRange(Player player)
    {
        return BaseController.IsInRange(player.ReferenceHub);
    }

    /// <summary>
    /// Interact with the workstation.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> that interacted.</param>
    public void Interact(Player player)
    {
        BaseController.ServerInteract(player.ReferenceHub, BaseController.ActivateCollider.ColliderId);
    }
}
