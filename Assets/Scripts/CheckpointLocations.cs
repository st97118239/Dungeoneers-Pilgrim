using System.Collections.Generic;
using UnityEngine;

public static class CheckpointLocations
{
    private static readonly Dictionary<Checkpoints, Vector3> positions = new() { 
        { Checkpoints.Spawn, new Vector3(2f, 1f, 0f) },
        { Checkpoints.Lantern, new Vector3(-20.5f, 1f, -30.75f) },
        { Checkpoints.AxeRoom, new Vector3(-40,1,-19f) },
        { Checkpoints.BarrelRoom, new Vector3(-67.5f, 1f, -4.75f) },
        { Checkpoints.HammerRoom, new Vector3(-56.5f, 1f, -29.5f) },
        { Checkpoints.EggRoom, new Vector3(-72f, 1f, -49.25f) }
    };

    private static readonly Dictionary<Checkpoints, Quaternion> rotations = new() {
        { Checkpoints.Spawn, Quaternion.Euler(0f, 90f, 0f) }, 
        { Checkpoints.Lantern, Quaternion.Euler(0f, 270f, 0f) },
        { Checkpoints.AxeRoom, Quaternion.Euler(0f, 0f, 0f) },
        { Checkpoints.BarrelRoom, Quaternion.Euler(0f, 180f, 0f) },
        { Checkpoints.HammerRoom, Quaternion.Euler(0f, 180f, 0f) },
        { Checkpoints.EggRoom, Quaternion.Euler(0f, 180f, 0f) }
    };

    public static Vector3 GetPosition(Checkpoints checkpoint)
    {
        return positions[checkpoint];
    }

    public static Quaternion GetRotation(Checkpoints checkpoint)
    {
        return rotations[checkpoint];
    }
}
