namespace LabApi.Features.Enums
{
    /// <summary>
    /// Enum type that represents a side, useful for seeing if someone can harm another person when comparing these together.
    /// </summary>
    public enum Side
    {
        /// <summary>
        /// The person's side is part of the Foundation. i.e. Scientists, Facility guards and the MTF force.
        /// </summary>
        Foundation,
        /// <summary>
        /// The person's side is part of the Chaos Insurgency. i.e. Class-D and the Chaos force.
        /// </summary>
        Chaos,
        /// <summary>
        /// The person's side is SCPs. This includes all SCPs.
        /// </summary>
        Scp,
        /// <summary>
        /// The person is a tutorial.
        /// </summary>
        Tutorial,
        /// <summary>
        /// The person has an unknown side.
        /// </summary>
        None
    }
}