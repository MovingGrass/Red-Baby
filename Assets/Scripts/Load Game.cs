using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        SaveManager.ClearSaveData();
        SceneManager.LoadScene("GameScene"); // Replace with your game scene name
    }

    public void LoadGame()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "savegame.json")))
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.Log("No save game found.");
        }
    }
}

