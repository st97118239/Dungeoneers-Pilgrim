using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class PlayerData
{
    public float health;
    public Vector3 position;
    public int score;
}

public class SaveLoadsystem : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        // Set the file path (you can adjust this to your preference)
        saveFilePath = Path.Combine(Application.persistentDataPath, "playerSaveData.json");
    }

    // Save data to a file
    public void SavePlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game Saved!");
    }

    // Load data from a file
    public PlayerData LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Game Loaded!");
            return playerData;
        }
        else
        {
            Debug.Log("No save file found.");
            return null;
        }
    }
}
