using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeSceneString(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeSceneindex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
