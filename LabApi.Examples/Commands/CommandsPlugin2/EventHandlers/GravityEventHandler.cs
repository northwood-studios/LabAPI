using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using PlayerRoles.FirstPersonControl;
using UnityEngine;
using MEC;

namespace CommandsPlugin2.EventHandlers
{
    /// <summary>
    /// Class used for handling any default gravity changes aswell as applying default gravity to players whose role has changed.
    /// </summary>
    public class GravityEventHandler : CustomEventsHandler
    {
        /// <summary>
        /// New default gravity for every player whose role changes.
        /// </summary>
        public Vector3 NewDefaultGravity
        {
            get
            {
                return _newDefaultGravity;
            }
            set
            {
                _newDefaultGravity = value;

                foreach (Player player in Player.List)
                {
                    if (!player.IsAlive)
                        continue;

                    player.Gravity = _newDefaultGravity;
                }
            }
        }

        private Vector3 _newDefaultGravity = FpcGravityController.DefaultGravity;

        public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
        {
            Timing.CallDelayed(0.1f, () => // Delay is required as it would get send before client receives the new role - may cause issues
            {
                if (!ev.Player.IsAlive)
                    return;

                ev.Player.Gravity = NewDefaultGravity;
            });
        }
    }
}
