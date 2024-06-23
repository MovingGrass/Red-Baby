using System.Collections.Generic; 
using UnityEngine; 

[System.Serializable]
public class GameData
{
    public List<CrystalData> collectedCrystals;
    public List<bool> gameObjectStates;
    public Vector3 playerPosition;
    public Vector3 enemyPosition1;
    public Vector3 enemyPosition2;

    public GameData()
    {
        gameObjectStates = new List<bool>();
        collectedCrystals = new List<CrystalData>();
    }
}


