using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public GameData gameData;
    public ExitDoor exitDoor;

    public GameObject player;
    public GameObject enemy1; // Baby
    public GameObject enemy2; // Baby (1)

    public CameraMovement playerCamera;
    public Flashlight flashlightScript;

    public List<GameObject> objectsToTrack;

    public List<Doors> doorsToTrack;

    private void Start()
    {
        Time.timeScale = 1f; // Ensure the game is not paused at start

        if (PlayerPrefs.HasKey("volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            AudioListener.volume = 1f; 
        }

        if (SaveManager.SaveFileExists())
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        gameData = new GameData();

        // 1. Save Positions (Using Floats for Build Stability)
        gameData.playerPos = new float[] { player.transform.position.x, player.transform.position.y, player.transform.position.z };
        gameData.enemy1Pos = new float[] { enemy1.transform.position.x, enemy1.transform.position.y, enemy1.transform.position.z };
        gameData.enemy2Pos = new float[] { enemy2.transform.position.x, enemy2.transform.position.y, enemy2.transform.position.z };

        // 2. Save Look Rotation
        // Player Body Y rotation + Camera X rotation
        gameData.lookRotation = new float[] { player.transform.eulerAngles.y, playerCamera.GetXRotation() };

        // 3. Save Flashlight
        gameData.isFlashlightOn = flashlightScript.IsOn();

        // 4. Save Crystals & Objects
        gameData.collectedCrystals = exitDoor.GetCollectedCrystals();
        
        gameData.gameObjectStates = new List<bool>();
        foreach (var obj in objectsToTrack)
        {
            gameData.gameObjectStates.Add(obj.activeSelf);
        }

        // 5. Save Doors
        gameData.doorStates = new List<bool>();
        foreach (var door in doorsToTrack)
        {
            gameData.doorStates.Add(door.IsOpen());
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

            // 1. Load Positions
            if (gameData.playerPos != null)
                player.transform.position = new Vector3(gameData.playerPos[0], gameData.playerPos[1], gameData.playerPos[2]);
            if (gameData.enemy1Pos != null)
                enemy1.transform.position = new Vector3(gameData.enemy1Pos[0], gameData.enemy1Pos[1], gameData.enemy1Pos[2]);
            if (gameData.enemy2Pos != null)
                enemy2.transform.position = new Vector3(gameData.enemy2Pos[0], gameData.enemy2Pos[1], gameData.enemy2Pos[2]);

            // 2. Load Look Rotation
            if (gameData.lookRotation != null && gameData.lookRotation.Length >= 2)
            {
                // Rotate Player Body (Y axis)
                Vector3 rot = player.transform.rotation.eulerAngles;
                player.transform.rotation = Quaternion.Euler(rot.x, gameData.lookRotation[0], rot.z);
                
                // Rotate Camera (X axis - Up/Down)
                playerCamera.SetLookRotation(gameData.lookRotation[1]);
            }

            // 3. Load Flashlight
            if(flashlightScript != null)
            {
                flashlightScript.SetFlashlightState(gameData.isFlashlightOn);
            }

            // 4. Load Crystals & Objects
            exitDoor.SetCollectedCrystals(gameData.collectedCrystals);
            
            if (gameData.gameObjectStates != null && gameData.gameObjectStates.Count == objectsToTrack.Count)
            {
                for (int i = 0; i < objectsToTrack.Count; i++)
                {
                    objectsToTrack[i].SetActive(gameData.gameObjectStates[i]);
                }
            }

            // 5. Load Doors
            if (gameData.doorStates != null && gameData.doorStates.Count == doorsToTrack.Count)
            {
                for (int i = 0; i < doorsToTrack.Count; i++)
                {
                    doorsToTrack[i].SetDoorState(gameData.doorStates[i]);
                }
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


