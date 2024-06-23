using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public GameData gameData;
    public ExitDoor exitDoor;

    public GameObject player;
    public GameObject enemy1; // Baby
    public GameObject enemy2; // Baby (1)

    public List<GameObject> objectsToTrack;

    private void Start()
    {
        Time.timeScale = 1f; // Ensure the game is not paused at start
        if (SaveManager.SaveFileExists())
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        gameData = new GameData
        {
            collectedCrystals = exitDoor.GetCollectedCrystals(),
            playerPosition = player.transform.position,
            enemyPosition1 = enemy1.transform.position,
            enemyPosition2 = enemy2.transform.position
        };

        gameData.gameObjectStates = new List<bool>();
        foreach (var obj in objectsToTrack)
        {
            gameData.gameObjectStates.Add(obj.activeSelf);
        }

        SaveManager.SaveGame(gameData);
    }

    public void LoadGame()
    {
        ClearState();
        GameData loadedData = SaveManager.LoadGame();
        if (loadedData != null)
        {
            gameData = loadedData;

            exitDoor.SetCollectedCrystals(gameData.collectedCrystals);
            player.transform.position = gameData.playerPosition;
            enemy1.transform.position = gameData.enemyPosition1;
            enemy2.transform.position = gameData.enemyPosition2;

            for (int i = 0; i < objectsToTrack.Count; i++)
            {
                objectsToTrack[i].SetActive(gameData.gameObjectStates[i]);
            }
        }
        EnablePlayerMovement();
    }

    public void SaveAndExit()
    {
        SaveGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void ClearState()
    {
        // Reset all tracked objects
        foreach (var obj in objectsToTrack)
        {
            obj.SetActive(false);
        }

        // Reset player and enemy positions if needed
        player.transform.position = Vector3.zero;
        enemy1.transform.position = Vector3.zero;
        enemy2.transform.position = Vector3.zero;

        // Clear collected crystals
        exitDoor.SetCollectedCrystals(new List<CrystalData>());
    }

    private void InitializeGame()
    {
        // Initialize game based on whether we should load or start new
        if (SaveManager.SaveFileExists())
        {
            LoadGame();
        }
        else
        {
            // Start new game logic if needed
            ClearState();
        }
    }
    private void EnablePlayerMovement()
    {
        // Assuming player has a movement script, re-enable it
        var playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Any other initializations needed to enable gameplay
        // Example: Resetting any necessary states
        Time.timeScale = 1f; // Ensure the game is not paused
    }
}


