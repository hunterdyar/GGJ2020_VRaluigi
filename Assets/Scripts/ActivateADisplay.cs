using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateADisplay : MonoBehaviour
{
    public int displayID;
    void Start()
    {
        Display.displays[displayID].Activate();
    }
}
