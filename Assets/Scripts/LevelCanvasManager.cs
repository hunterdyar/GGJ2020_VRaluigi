using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCanvasManager : MonoBehaviour
{
    public GameObject[] disabledOnStart;
    public GameObject[] enabledOnStart;
    public GameObject[] enabledOnDeath;
    bool started = false;
    void Start(){
        foreach(GameObject go in disabledOnStart)
        {
            go.SetActive(true);
        }
        foreach(GameObject go in enabledOnDeath)
        {
            go.SetActive(false);
        }
    }
    public void OnDeath()
    {
        foreach(GameObject go in enabledOnDeath)
        {
            go.SetActive(true);
        }
    }
    public void StartGame()
    {
        foreach(GameObject go in disabledOnStart)
        {
            go.SetActive(false);
        }
        foreach(GameObject go in enabledOnStart)
        {
            go.SetActive(true);
        }
    }
    void Update()
    {
        if(started){return;}
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
            started = true;
        }
    }
}
