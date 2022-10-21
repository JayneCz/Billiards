using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static object KeptData { get; private set; } = null;
    public static void LoadScene(string sceneName, object dataToKeep = null)
    {
        KeptData = dataToKeep;
        SceneManager.LoadScene(sceneName);
    }
    public void LoadScene(string sceneName)
    {
        SceneSwitcher.LoadScene(sceneName);
    }
}
