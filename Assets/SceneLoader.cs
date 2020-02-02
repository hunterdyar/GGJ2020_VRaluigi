using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public string[] sceneNamesToLoad;

    void Start()
    {

        foreach(string s in sceneNamesToLoad)
        {
            SceneManager.LoadSceneAsync(s,LoadSceneMode.Additive);
        }
    }
}
