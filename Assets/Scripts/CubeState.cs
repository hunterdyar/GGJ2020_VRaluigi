using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public int row = 0;
    public int col = 0;
    public int dep = 0;

    void OnTriggerEnter(Collider other)
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    void OnTriggerExit(Collider other) {
        this.GetComponent<MeshRenderer>().material.color = Color.grey;
    }

}
