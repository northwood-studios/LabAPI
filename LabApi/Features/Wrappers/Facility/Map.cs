using MapGeneration;
using MapGeneration.Distributors;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Represents the map of the facility.
/// </summary>
public static class Map
{
    /// <summary>
    /// Gets the current seed of the map.
    /// </summary>
    public static int Seed => SeedSynchronizer.Seed;

    /// <summary>
    /// Gets all the <see cref="Room">rooms</see>.
    /// </summary>
    public static IReadOnlyCollection<Room> Rooms => Room.List;

    /// <summary>
    /// Gets all the <see cref="RoomLight">room lights</see>.
    /// </summary>
    public static IReadOnlyCollection<LightsController> RoomLights => LightsController.List;

    /// <summary>
    /// Gets all the <see cref="Camera">cameras</see>.
    /// </summary>
    public static IReadOnlyCollection<Camera> Cameras => Camera.List;

    /// <summary>
    /// Gets all the <see cref="Door">doors</see>.
    /// </summary>
    public static IReadOnlyCollection<Door> Doors => Door.List;

    /// <summary>
    /// Gets all the <see cref="Elevator">elevators</see>.
    /// </summary>
    public static IReadOnlyCollection<Elevator> Elevators => Elevator.List;

    /// <summary>
    /// Gets all the <see cref="Tesla">teslas</see>.
    /// </summary>
    public static IReadOnlyCollection<Tesla> Teslas => Tesla.List;

    /// <summary>
    /// Gets all the <see cref="Generator">generators</see>.
    /// </summary>
    public static IReadOnlyCollection<Generator> Generators => Generator.List;

    /// <summary>
    /// Gets all the <see cref="Pickup">pickups</see>.
    /// </summary>
    public static IReadOnlyCollection<Pickup> Pickups => Pickup.List;

    /// <summary>
    /// Gets all the <see cref="Ragdoll">ragdolls</see>.
    /// </summary>
    public static IReadOnlyCollection<Ragdoll> Ragdolls => Ragdoll.List;

    /// <summary>
    /// Represents the bounds for the default escape zone on surface.
    /// </summary>
    /// <remarks>
    /// By default this is included in the <see cref="EscapeZones"/> list.
    /// </remarks>
    public static Bounds DefaultEscapeZone { get; } = Escape.DefaultEscapeZone;

    /// <summary>
    /// A list of all bounds used as escape zones.
    /// </summary>
    /// <remarks>
    /// By default only the <see cref="DefaultEscapeZone"/> is included in the list.
    /// </remarks>
    public static List<Bounds> EscapeZones => Escape.EscapeZones;

    /// <summary>
    /// Adds another bounds to be used as an escape zone to the <see cref="EscapeZones"/> list.
    /// </summary>
    /// <param name="escapeZone">The bounds of the new escape zone.</param>
    public static void AddEscapeZone(Bounds escapeZone) => EscapeZones.Add(escapeZone);

    /// <summary>
    /// Removes an existing bounds from the <see cref="EscapeZones"/> list.
    /// </summary>
    /// <param name="escapeZone">The bounds of the escape zone to remove.</param>
    public static void RemoveEscapeZone(Bounds escapeZone) => EscapeZones.Remove(escapeZone);

    #region Get Random

    /// <summary>
    /// Gets a random <see cref="Room"/>.
    /// </summary>
    /// <returns>The random room if there were any rooms otherwise see langword="null"/>.</returns>
    public static Room? GetRandomRoom()
    {
        return Rooms.Count != 0 ? Rooms.ElementAt(UnityEngine.Random.Range(0, Rooms.Count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Room"/> in a zone.
    /// </summary>
    /// <param name="zone">The zone to pick a random room from.</param>
    /// <returns>The random room if there were any rooms in the zone otherwise null.</returns>
    public static Room? GetRandomRoom(FacilityZone zone)
    {
        // TODO: use zone wrapper.
        IEnumerable<Room> rooms = Rooms.Where(x => x.Zone == zone);
        int count = rooms.Count();
        return count != 0 ? rooms.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Room"/> in the zones.
    /// </summary>
    /// <param name="zones">The zones to pick a random room from.</param>
    /// <returns>The random room if there were any rooms in the zones otherwise null.</returns>
    public static Room? GetRandomRoom(IEnumerable<FacilityZone> zones)
    {
        // TODO: use zone wrapper.
        IEnumerable<Room> rooms = Rooms.Where(x => zones.Contains(x.Zone));
        int count = rooms.Count();
        return count != 0 ? rooms.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Door"/>.
    /// </summary>
    /// <returns>The random door if there were any doors otherwise null.</returns>
    public static Door? GetRandomDoor()
    {
        return Doors.Count != 0 ? Doors.ElementAt(UnityEngine.Random.Range(0, Doors.Count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Door"/> in a zone.
    /// </summary>
    /// <param name="zone">The zone to pick a random door from.</param>
    /// <param name="includeBorders">Whether or not to include doors that are in-between 2 zones.</param>
    /// <returns>The random door if there were any in doors the zone otherwise null.</returns>
    public static Door? GetRandomDoor(FacilityZone zone, bool includeBorders)
    {
        // TODO: use zone wrapper.
        IEnumerable<Door> doors = Doors.Where(x => includeBorders ? x.Rooms.Any(x => x.Zone == zone) : x.Rooms.All(x => x.Zone == zone));
        int count = doors.Count();
        return count != 0 ? doors.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Door"/> in the zones.
    /// </summary>
    /// <param name="zones">The zones to pick a random door from.</param>
    /// <param name="includeBorders">Whether or not to include doors that are in-between 2 zones.</param>
    /// <returns>The random door if there were any doors in the zones otherwise null.</returns>
    public static Door? GetRandomDoor(IEnumerable<FacilityZone> zones, bool includeBorders)
    {
        // TODO: use zone wrapper.
        IEnumerable<Door> doors = Doors.Where(x => includeBorders ? x.Rooms.Any(x => zones.Contains(x.Zone)) : x.Rooms.All(x => zones.Contains(x.Zone)));
        int count = doors.Count();
        return count != 0 ? doors.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="RoomLight"/>.
    /// </summary>
    /// <returns>The random room light if there were any room lights otherwise null.</returns>
    public static LightsController? GetRandomLight()
    {
        return RoomLights.Count != 0 ? RoomLights.ElementAt(UnityEngine.Random.Range(0, RoomLights.Count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="RoomLight"/> in a zone.
    /// </summary>
    /// <param name="zone">The zone to pick a random room light from.</param>
    /// <returns>The random room light if there were any room lights in the zone otherwise null.</returns>
    public static LightsController? GetRandomLight(FacilityZone zone)
    {
        // TODO: use zone wrapper.
        IEnumerable<LightsController> lights = RoomLights.Where(x => x.Room.Zone == zone);
        int count = lights.Count();
        return count != 0 ? lights.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="RoomLight"/> in the zones.
    /// </summary>
    /// <param name="zones">The zones to pick a random room light from.</param>
    /// <returns>The random room light if there were any room lights in the zones otherwise null.</returns>
    // TODO: use light wrapper
    public static LightsController? GetRandomLight(IEnumerable<FacilityZone> zones)
    {
        // TODO: use zone wrapper.
        IEnumerable<LightsController> lights = RoomLights.Where(x => zones.Contains(x.Room.Zone));
        int count = lights.Count();
        return count != 0 ? lights.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Camera"/>.
    /// </summary>
    /// <returns>The random camera if there were any cameras otherwise null.</returns>
    public static Camera? GetRandomCamera()
    {
        return Cameras.Count != 0 ? Cameras.ElementAt(UnityEngine.Random.Range(0, Cameras.Count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Camera"/> in a zone.
    /// </summary>
    /// <param name="zone">The zone to pick a random camera from.</param>
    /// <returns>The random camera if there were any cameras in the zone otherwise null.</returns>
    public static Camera? GetRandomCamera(FacilityZone zone)
    {
        // TODO: use zone wrapper.
        IEnumerable<Camera> cameras = Cameras.Where(x => x.Room.Zone == zone);
        int count = cameras.Count();
        return count != 0 ? cameras.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Camera"/> in the zones.
    /// </summary>
    /// <param name="zones">The zones to pick a random camera from.</param>
    /// <returns>The random camera if there were any cameras in the zones otherwise null.</returns>
    public static Camera? GetRandomCamera(IEnumerable<FacilityZone> zones)
    {
        // TODO: use zone wrapper.
        IEnumerable<Camera> cameras = Cameras.Where(x => zones.Contains(x.Room.Zone));
        int count = cameras.Count();
        return count != 0 ? cameras.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Locker"/>.
    /// </summary>
    /// <returns>The random locker if there were any lockers otherwise null.</returns>
    // TODO: use wrapper type
    public static Locker? GetRandomLocker()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a random <see cref="Locker"/> in a zone.
    /// </summary>
    /// <param name="zone">The zone to pick a random locker from.</param>
    /// <returns>The random locker if there were any lockers in the zone otherwise null.</returns>
    // TODO: use wrapper type
    public static Locker? GetRandomLocker(FacilityZone zone)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a random <see cref="Locker"/>.
    /// </summary>
    /// <param name="zones">The zones to pick a random locker from.</param>
    /// <returns>The random locker if there were any lockers in the zones otherwise null.</returns>
    // TODO: use wrapper type
    public static Locker? GetRandomLocker(IEnumerable<FacilityZone> zones)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a random <see cref="Elevator"/>.
    /// </summary>
    /// <returns>The random elevator if there were any elevators otherwise null.</returns>
    public static Elevator? GetRandomElevator()
    {
        return Elevators.Count != 0 ? Elevators.ElementAt(UnityEngine.Random.Range(0, Elevators.Count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Tesla"/>.
    /// </summary>
    /// <returns>The random tesla if there were any teslas otherwise null.</returns>
    public static Tesla? GetRandomTesla()
    {
        return Teslas.Count != 0 ? Teslas.ElementAt(UnityEngine.Random.Range(0, Teslas.Count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Generator"/>.
    /// </summary>
    /// <returns>The random generator if there were any generators otherwise null.</returns>
    public static Generator? GetRandomGenerator()
    {
        return Generators.Count != 0 ? Generators.ElementAt(UnityEngine.Random.Range(0, Generators.Count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Pickup"/>.
    /// </summary>
    /// <returns>The random pickup if there were any pickups otherwise null.</returns>
    public static Pickup? GetRandomPickup()
    {
        return Pickups.Count != 0 ? Pickups.ElementAt(UnityEngine.Random.Range(0, Pickups.Count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Pickup"/> in a zone.
    /// </summary>
    /// <param name="zone">The zone to pick a random pickup from.</param>
    /// <returns>The random pickup if there were any in the zone otherwise null.</returns>
    // TODO: implement once pickup is given a Room property
    public static Pickup? GetRandomPickup(FacilityZone zone)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a random <see cref="Pickup"/> in the zones.
    /// </summary>
    /// <param name="zones">The zones to pick a random pickup form.</param>
    /// <returns>The random pickup if there were any in the zones otherwise null.</returns>
    // TODO: implement once pickup is given a Room property
    public static Pickup? GetRandomPickup(IEnumerable<FacilityZone> zones)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a random <see cref="Ragdoll"/>.
    /// </summary>
    /// <returns>The random ragdoll if there were any otherwise null.</returns>
    public static Ragdoll? GetRandomRagdoll()
    {
        return Ragdolls.Count != 0 ? Ragdolls.ElementAt(UnityEngine.Random.Range(0, Ragdolls.Count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Ragdoll"/> in a zone.
    /// </summary>
    /// <param name="zone">The zone to pick a random ragdoll from.</param>
    /// <returns>The random ragdoll if there were any in the zone otherwise null.</returns>
    public static Ragdoll? GetRandomRagdoll(FacilityZone zone)
    {
        // TODO: use zone wrapper.
        IEnumerable<Ragdoll> ragdolls = Ragdolls.Where(x => Room.TryGetRoomAtPosition(x.Position, out Room? room) && room.Zone == zone);
        int count = ragdolls.Count();
        return count != 0 ? ragdolls.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }

    /// <summary>
    /// Gets a random <see cref="Ragdoll"/> in the zones.
    /// </summary>
    /// <param name="zones">The zones to pick a random ragdoll from.</param>
    /// <returns>The random ragdoll if there were any in the zones otherwise null.</returns>
    public static Ragdoll? GetRandomRagdoll(IEnumerable<FacilityZone> zones)
    {
        // TODO: use zone wrapper.
        IEnumerable<Ragdoll> ragdolls = Ragdolls.Where(x => Room.TryGetRoomAtPosition(x.Position, out Room? room) && zones.Contains(room.Zone));
        int count = ragdolls.Count();
        return count != 0 ? ragdolls.ElementAt(UnityEngine.Random.Range(0, count)) : null;
    }
    #endregion

    #region Lights

    /// <summary>
    /// Turns off all lights for a certain duration.
    /// </summary>
    /// <param name="duration">How long to keep the lights off.</param>
    public static void TurnOffLights(float duration)
    {
        foreach (LightsController lc in LightsController.List)
        {
            lc.FlickerLights(duration);
        }
    }

    /// <summary>
    /// Turns off all lights.
    /// </summary>
    public static void TurnOffLights() => TurnOffLights(float.MaxValue);

    /// <summary>
    /// Turns off lights in a zone.
    /// </summary>
    /// <param name="zone">The zone to turn the lights off in.</param>
    public static void TurnOffLights(FacilityZone zone)
    {
        // TODO: use zone wrapper?
        foreach (LightsController lc in LightsController.List)
        {
            if (lc.Room.Zone != zone)
            {
                continue;
            }

            lc.FlickerLights(float.MaxValue);
        }
    }

    /// <summary>
    /// Turns off lights in the zones.
    /// </summary>
    /// <param name="zones">The zones to turn off the lights.</param>
    public static void TurnOffLights(IEnumerable<FacilityZone> zones)
    {
        IEnumerable<FacilityZone> facilityZones = zones as FacilityZone[] ?? zones.ToArray();

        // TODO: use zone wrapper.
        foreach (LightsController lc in LightsController.List)
        {
            if (!facilityZones.Contains(lc.Room.Zone))
            {
                continue;
            }

            lc.FlickerLights(float.MaxValue);
        }
    }

    /// <summary>
    /// Turns off all lights in a zone for a certain duration.
    /// </summary>
    /// <param name="duration">How long to keep the lights off.</param>
    /// <param name="zone">The zone to turn the lights off in.</param>
    public static void TurnOffLights(float duration, FacilityZone zone)
    {
        // TODO: use zone wrapper.
        foreach (LightsController lc in LightsController.List)
        {
            if (lc.Room.Zone != zone)
            {
                continue;
            }

            lc.FlickerLights(duration);
        }
    }

    /// <summary>
    /// Turns off all lights in the zones for a certain duration.
    /// </summary>
    /// <param name="duration">How long to keep the lights off.</param>
    /// <param name="zones">The zones to turn the lights off in.</param>
    public static void TurnOffLights(float duration, IEnumerable<FacilityZone> zones)
    {
        // TODO: use zone wrapper.
        foreach (LightsController lc in LightsController.List)
        {
            if (!zones.Contains(lc.Room.Zone))
            {
                continue;
            }

            lc.FlickerLights(duration);
        }
    }

    /// <summary>
    /// Turns on all the lights.
    /// </summary>
    public static void TurnOnLights()
    {
        foreach (LightsController lc in LightsController.List)
        {
            lc.LightsEnabled = true;
        }
    }

    /// <summary>
    /// Turns on all the lights in a zone.
    /// </summary>
    /// <param name="zone">The zone to turn all the light on in.</param>
    public static void TurnOnLights(FacilityZone zone)
    {
        // TODO: use zone wrapper.
        foreach (LightsController lc in LightsController.List)
        {
            if (lc.Room.Zone != zone)
            {
                continue;
            }

            lc.LightsEnabled = true;
        }
    }

    /// <summary>
    /// Turns on all the lights in the zones.
    /// </summary>
    /// <param name="zones">The zones to turn all the lights on in.</param>
    public static void TurnOnLights(IEnumerable<FacilityZone> zones)
    {
        // TODO: use zone wrapper.
        foreach (LightsController lc in LightsController.List)
        {
            if (!zones.Contains(lc.Room.Zone))
            {
                continue;
            }

            lc.LightsEnabled = true;
        }
    }

    // TODO: Room.Lights could support a Lights TurnOn with duration possibly

    /// <summary>
    /// Sets the color of all the lights.
    /// </summary>
    /// <param name="color">The color to set the lights.</param>
    public static void SetColorOfLights(UnityEngine.Color color)
    {
        foreach (LightsController lc in LightsController.List)
        {
            lc.OverrideLightsColor = color;
        }
    }

    /// <summary>
    /// Sets the color of all the lights in a zone.
    /// </summary>
    /// <param name="color">The color to set the lights.</param>
    /// <param name="zone">The zone to effect.</param>
    public static void SetColorOfLights(UnityEngine.Color color, FacilityZone zone)
    {
        // TODO: use zone wrapper.
        foreach (LightsController lc in LightsController.List)
        {
            if (lc.Room.Zone != zone)
            {
                continue;
            }

            lc.OverrideLightsColor = color;
        }
    }

    /// <summary>
    /// Sets the color of all the lights in the zones.
    /// </summary>
    /// <param name="color">The color to set the lights.</param>
    /// <param name="zones">The zones to effect.</param>
    public static void SetColorOfLights(UnityEngine.Color color, IEnumerable<FacilityZone> zones)
    {
        // TODO: use zone wrapper.
        foreach (LightsController lc in LightsController.List)
        {
            if (!zones.Contains(lc.Room.Zone))
            {
                continue;
            }

            lc.OverrideLightsColor = color;
        }
    }

    /// <summary>
    /// Sets the color of all the lights back to their default.
    /// </summary>
    public static void ResetColorOfLights()
    {
        foreach (LightsController lc in LightsController.List)
        {
            lc.OverrideLightsColor = UnityEngine.Color.clear;
        }
    }

    /// <summary>
    /// Sets the color of all the lights in a zone back to their default.
    /// </summary>
    /// <param name="zone">The zone to effect.</param>
    public static void ResetColorOfLights(FacilityZone zone)
    {
        // TODO: use zone wrapper.
        foreach (LightsController lc in LightsController.List)
        {
            if (lc.Room.Zone != zone)
            {
                continue;
            }

            lc.OverrideLightsColor = UnityEngine.Color.clear;
        }
    }

    /// <summary>
    /// Sets the color of all lights in the zones back to their default.
    /// </summary>
    /// <param name="zones">The zones to effect.</param>
    public static void ResetColorOfLights(IEnumerable<FacilityZone> zones)
    {
        // TODO: use zone wrapper.
        foreach (LightsController lc in LightsController.List)
        {
            if (!zones.Contains(lc.Room.Zone))
            {
                continue;
            }

            lc.OverrideLightsColor = UnityEngine.Color.clear;
        }
    }
    #endregion
}
