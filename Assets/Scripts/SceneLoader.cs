using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public string[] sceneNamesToLoad;

    void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        foreach(string s in sceneNamesToLoad)
        {
            SceneManager.LoadSceneAsync(s,LoadSceneMode.Additive);
        }
    }
    public void RestartGame()
    {
        foreach(string s in sceneNamesToLoad)
        {
            SceneManager.UnloadSceneAsync(s);
        }
        StartGame();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}
