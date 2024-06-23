using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string saveFilePath;

    static SaveManager()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
    }

    public static void SaveGame(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFilePath, json);
    }

    public static GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }

    public static void ClearSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }

    public static bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }
}





