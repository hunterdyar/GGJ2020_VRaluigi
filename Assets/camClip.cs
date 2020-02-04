using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camClip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var camTarget = GetComponent<Camera>();
        camTarget.nearClipPlane = 0.01f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
