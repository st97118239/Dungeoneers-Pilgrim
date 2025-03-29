using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]

public struct SaveData
{
    public Vector3 position;
    public int coins;
    public float health;
    public string[] inventory;

    public SaveData(float health, int coins, Vector3 position, string[] inventory)
    {
        this.health = health;
        this.coins = coins;
        this.position = position;
        this.inventory = inventory;
    }
}