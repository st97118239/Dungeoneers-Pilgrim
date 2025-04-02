using System.Collections.Generic;
using UnityEngine;

public static class DefaultWeaponLocations
{
    private static readonly Dictionary<WeaponType, Vector3> positions = new() { 
        { WeaponType.Sword, new Vector3(0.55f, -0.5f, 0.64f) },
        { WeaponType.Axe, new Vector3(0.55f, -0.2f, 0.64f) },
        { WeaponType.Hammer, new Vector3(0.4f, -0.68f, 0.65f) },
        { WeaponType.Scythe, new Vector3(0.26f, -0.90f, 0.67f) }
    };

    private static readonly Dictionary<WeaponType, Quaternion> rotations = new() {
        { WeaponType.Sword, Quaternion.Euler(6.33f, 0, 0) }, 
        { WeaponType.Axe, Quaternion.Euler(358.36f, 269.46f, 347.9f) },
        { WeaponType.Hammer, Quaternion.Euler(339.84f, 263.95f, 2.21f) },
        { WeaponType.Scythe, Quaternion.Euler(5.20f, 181f, 20.29f) }
    };

    public static Vector3 GetPosition(WeaponType weaponType)
    {
        return positions[weaponType];
    }

    public static Quaternion GetRotation(WeaponType weaponType)
    {
        return rotations[weaponType];
    }
}
