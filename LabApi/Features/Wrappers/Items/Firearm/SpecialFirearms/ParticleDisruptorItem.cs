using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static InventorySystem.Items.Firearms.Modules.DisruptorActionModule;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ParticleDisruptor"/>.
/// </summary>
public class ParticleDisruptorItem : FirearmItem
{
    /// <summary>
    /// Contains all the cached particle disruptor items, accessible through their <see cref="ParticleDisruptor"/>.
    /// </summary>
    public static new Dictionary<ParticleDisruptor, ParticleDisruptorItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="ParticleDisruptorItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<ParticleDisruptorItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the particle disruptor item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="ParticleDisruptor"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="particleDisruptor">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(particleDisruptor))]
    public static ParticleDisruptorItem? Get(ParticleDisruptor? particleDisruptor)
    {
        if (particleDisruptor == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(particleDisruptor, out ParticleDisruptorItem item) ? item : (ParticleDisruptorItem)CreateItemWrapper(particleDisruptor);
    }

    private DisruptorModeSelector _selectorModule = null!;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="particleDisruptor">The base <see cref="ParticleDisruptor"/> object.</param>
    internal ParticleDisruptorItem(ParticleDisruptor particleDisruptor)
        : base(particleDisruptor)
    {
        Base = particleDisruptor;

        if (CanCache)
        {
            Dictionary.Add(particleDisruptor, this);
        }
    }

    /// <summary>
    /// The base <see cref="ParticleDisruptor"/> object.
    /// </summary>
    public new ParticleDisruptor Base { get; }

    /// <summary>
    /// Gets the current firing state.
    /// </summary>
    public FiringState FiringState
    {
        get
        {
            if (ActionModule is DisruptorActionModule actionModule)
            {
                return actionModule.CurFiringState;
            }

            return FiringState.None;
        }
    }

    /// <summary>
    /// Gets whether the disruptor has single-shot mode selected.
    /// </summary>
    public bool SingleShotMode
    {
        get
        {
            if (_selectorModule is DisruptorModeSelector selectorModule)
            {
                return selectorModule.SingleShotSelected;
            }

            return false;
        }
    }

    /// <summary>
    /// Gets the amount of chambered ammo in the chamber.
    /// </summary>
    public override int ChamberedAmmo
    {
        get
        {
            if (ActionModule is DisruptorActionModule actionModule)
            {
                return actionModule.IsLoaded ? 1 : 0;
            }

            return 0;
        }
    }

    /// <summary>
    /// Gets the maximum chambered ammo.
    /// </summary>
    public override int ChamberMax
    {
        get => 1;
    }

    /// <summary>
    /// Gets whether the firearm is cocked and can fire.
    /// </summary>
    public override bool Cocked
    {
        get
        {
            if (ActionModule is DisruptorActionModule actionModule)
            {
                return actionModule.IsLoaded;
            }

            return false;
        }
    }

    /// <inheritdoc/>
    public override bool OpenBolt => true;

    /// <summary>
    /// Destroys this disruptor and plays the destroy animation on the client.
    /// </summary>
    public void Destroy()
    {
        Base.ServerDestroyItem();
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <inheritdoc/>
    protected override void CacheModules()
    {
        base.CacheModules();

        foreach (ModuleBase module in Modules)
        {
            if (module is DisruptorModeSelector selectorModule)
            {
                _selectorModule = selectorModule;
                break;
            }
        }
    }
}
