using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    
    public List<CrystalData> collectedCrystals;
    public float[] playerPos;
    public float[] enemy1Pos;
    public float[] enemy2Pos;
    public List<bool> gameObjectStates;

   
    public float[] lookRotation; 
    public bool isFlashlightOn;
    public List<bool> doorStates;

    public GameData()
    {
        collectedCrystals = new List<CrystalData>();
        gameObjectStates = new List<bool>();
        doorStates = new List<bool>();
    }
}