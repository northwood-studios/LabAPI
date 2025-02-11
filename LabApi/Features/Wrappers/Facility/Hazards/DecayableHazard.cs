using Hazards;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers
{
    /// <summary>
    /// A class representing the <see cref="TemporaryHazard"/>.
    /// </summary>
    public class DecayableHazard : Hazard
    {
        /// <summary>
        /// Contains all the cached items, accessible through their <see cref="Base"/>.
        /// </summary>
        public new static Dictionary<TemporaryHazard, DecayableHazard> Dictionary { get; } = [];

        /// <summary>
        /// Gets all currently active decayable hazards.
        /// </summary>
        public new IReadOnlyCollection<DecayableHazard> List => Dictionary.Values;

        /// <summary>
		/// Gets or sets the modifier applied to <see cref="Time.deltaTime"/> when calculating how much time has passed.<br/>
        /// Setting this value will override any subclass modifiers. Setting it to values less than 0 will remove the override.
		/// </summary>
        public float DecaySpeed
        {
            get => Base.DecaySpeed;
            set => Base.DecaySpeed = value;
        }

        /// <summary>
		/// Gets or sets the amount of time this object will persist for (in seconds) before disappearing.
		/// </summary>
        public float LiveDuration
        {
            get => Base.HazardDuration;
            set => Base.HazardDuration = value;
        }

        /// <summary>
        /// Gets or sets the amount of time in seconds this hazard is being active.
        /// Doesn't progress if the <see cref="Hazard.IsActive"/> is set to <see langword="false"/>.
        /// </summary>
        public float Elapsed
        {
            get => Base.Elapsed;
            set => Base.Elapsed = value;
        }

        /// <summary>
        /// The base object.
        /// </summary>
        public TemporaryHazard Base { get; private set; }

        /// <summary>
        /// An internal constructor to prevent external instantiation.
        /// </summary>
        /// <param name="hazard">The base game object.</param>
        protected DecayableHazard(TemporaryHazard hazard) : base(hazard)
        {
            Base = hazard;
            Dictionary.Add(hazard, this);
        }

        /// <summary>
        /// Destroys this hazard.<br/>
        /// Do note that subclasses usually implement few seconds delay before the actual object is destroyed. (Usually to wait for animations to finish on clients)
        /// </summary>
        public override void Destroy() => Base.ServerDestroy();

        /// <inheritdoc/>
        internal override void OnRemove()
        {
            base.OnRemove();
            Dictionary.Remove(Base);
        }

        /// <summary>
        /// Gets the hazard wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="TemporaryHazard"/> was not <see langword="null"/>.
        /// </summary>
        /// <param name="hazard">The <see cref="Base"/> of the hazard.</param>
        /// <returns>The requested hazard or <see langword="null"/>.</returns>
        [return: NotNullIfNotNull(nameof(hazard))]
        public static DecayableHazard? Get(TemporaryHazard? hazard)
        {
            if (hazard == null)
                return null;

            return Dictionary.TryGetValue(hazard, out DecayableHazard decHazard) ? decHazard : (DecayableHazard)CreateItemWrapper(hazard);
        }
    }
}
