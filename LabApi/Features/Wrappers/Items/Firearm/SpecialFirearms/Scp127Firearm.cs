using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using InventorySystem.Items.Firearms.Modules.Scp127;
using System.Collections.Generic;
using UnityEngine;
using static InventorySystem.Items.Firearms.Modules.Scp127.Scp127VoiceTriggerBase;
using Logger = LabApi.Features.Console.Logger;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for SCP-127.
/// </summary>
public class Scp127Firearm : FirearmItem
{
    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="firearm">The base <see cref="Firearm"/> object.</param>
    internal Scp127Firearm(Firearm firearm) : base(firearm)
    {
    }

    /// <inheritdoc/>
    public override bool OpenBolt => false;

    /// <summary>
    /// Gets all players who are friended with this SCP.<br/>
    /// This means every player who talked with this SCP at least once and is still alive.
    /// </summary>
    public IEnumerable<Player> Friends
    {
        get
        {
            if (!Scp127VoiceLineManagerModule.FriendshipMemory.TryGetValue(Serial, out HashSet<uint> friendNetIds))
                yield break;

            foreach (uint netId in friendNetIds)
                yield return Player.Get(netId);
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="Scp127Tier"/> the firearm has.
    /// </summary>
    public Scp127Tier Tier
    {
        get
        {
            if (_tierModule == null)
                return Scp127Tier.Tier1;

            return _tierModule.CurTier;
        }
        set
        {
            if (_tierModule == null)
            {
                Logger.Error($"Unable to set {nameof(Tier)}, {nameof(Scp127TierManagerModule)} is null");
                return;
            }

            _tierModule.CurTier = value;
        }
    }

    /// <summary>
    /// Gets or sets the experience amount of this firearm.
    /// </summary>
    public float Experience
    {
        get
        {
            if (_tierModule == null)
                return 0;

            return _tierModule.ServerExp;
        }
        set
        {
            if (_tierModule == null)
            {
                Logger.Error($"Unable to set {nameof(Experience)}, {nameof(Scp127TierManagerModule)} is null");
                return;
            }

            _tierModule.ServerExp = value;
        }
    }

    /// <inheritdoc/>
    public override int ChamberedAmmo
    {
        get
        {
            if (_actionModule is Scp127ActionModule actionModule)
                return actionModule.AmmoStored;

            return 0;
        }
        set
        {
            if (_actionModule is not Scp127ActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(ChamberedAmmo)} as the action module is null.");
                return;
            }

            actionModule.ChamberSize = value;
            actionModule.ServerResync();
        }
    }

    /// <inheritdoc/>
    public override int ChamberMax
    {
        get
        {
            if (_actionModule is Scp127ActionModule actionModule)
                return actionModule.ChamberSize;

            return 0;
        }
        set
        {
            if (_actionModule is not Scp127ActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(ChamberMax)} as the action module is null.");
                return;
            }

            actionModule.ChamberSize = value;
        }
    }

    /// <inheritdoc/>
    public override bool MagazineInserted
    {
        get
        {
            return true;
        }
        set
        {
            if (_magazineControllerModule is Scp127MagazineModule magazineModule)
            {
                Logger.Error($"Unable to set {nameof(MagazineInserted)} as SCP-127's magazine is not detachable.");
                return;
            }
        }
    }


    /// <summary>
    /// Gets or sets the stored ammo in a <b>ammo container</b> for this firearm.
    /// </summary>
    /// <remarks>
    /// SCP-127 stored ammo is capped at <see cref="FirearmItem.MaxAmmo"/> and any values beyond are ignored.
    /// </remarks>
    public override int StoredAmmo
    {
        get => base.StoredAmmo;
        set => base.StoredAmmo = value;
    }

    private Scp127TierManagerModule _tierModule;

    private Scp127VoiceLineManagerModule _voiceModule;

    /// <summary>
    /// Plays a specific voiceline defined by the <see cref="Scp127VoiceLinesTranslation"/>.
    /// </summary>
    /// <param name="voiceline">The target voiceline to play.</param>
    /// <param name="priority">The priority to play this voice line with.</param>
    public void PlayVoiceline(Scp127VoiceLinesTranslation voiceline, VoiceLinePriority priority = VoiceLinePriority.Normal)
    {
        if (_voiceModule == null)
        {
            Logger.Error($"Unable to play voiceline, {nameof(Scp127VoiceLineManagerModule)} is null");
            return;
        }

        if (!_voiceModule.TryFindVoiceLine(voiceline, out Scp127VoiceTriggerBase trigger, out AudioClip clip))
        {
            Logger.Error($"Unable to play voiceline {voiceline} as it wasn't found");
            return;
        }

        _voiceModule.ServerSendVoiceLine(trigger, null, clip, (byte)priority);

    }

    /// <inheritdoc/>
    protected override void CacheModules()
    {
        base.CacheModules();

        foreach (ModuleBase module in Modules)
        {
            if (module is Scp127TierManagerModule tierModule)
            {
                _tierModule = tierModule;
                continue;
            }

            if (module is Scp127VoiceLineManagerModule voiceModule)
            {
                _voiceModule = voiceModule;
                continue;
            }
        }
    }
}
