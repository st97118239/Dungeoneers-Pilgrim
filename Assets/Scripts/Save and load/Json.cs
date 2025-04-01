using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class SaveSystem : MonoBehaviour
{
    //private static string saveFilePath = Application.persistentDataPath + "/savefile.json";

    //public static void Save(SaveData data)
    //{
    //    // Convert the data to a JSON string 
    //    string json = JsonUtility.ToJson(data);

    //    // Save the JSON string to a file 
    //    File.WriteAllText(saveFilePath, json);

    //    Debug.Log("Game Saved at " + saveFilePath);
    //}

    //public static SaveData Load()
    //{
    //    if (File.Exists(saveFilePath))
    //    {
    //        // Read the JSON string from the file 
    //        string json = File.ReadAllText(saveFilePath);

    //        // Deserialize the JSON into a SaveData object 
    //        SaveData loadedData = JsonUtility.FromJson<SaveData>(json);

    //        Debug.Log("Game Loaded from " + saveFilePath);
    //        return loadedData;
    //    }
    //    else
    //    {
    //        Debug.LogError("Save file does not exist.");
    //        return null; // Or return a default SaveData instance if preferred 
    //    }
    //}
}