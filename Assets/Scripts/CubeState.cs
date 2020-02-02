using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public int row = 0;
    public int col = 0;
    public int dep = 0;

    public int[] lastCollided;

    void OnTriggerEnter(Collider other)
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        var otherCube = other.gameObject.GetComponent<CubeState>();
        if (otherCube)
        {
            if (otherCube.row == this.row && otherCube.col == this.col && otherCube.dep == this.dep)
            {
                return;
            }
            lastCollided = new int[] { otherCube.row, otherCube.col, otherCube.dep };
            Debug.Log($"Last collide: x{lastCollided[0]}, y{lastCollided[1]}, z{lastCollided[2]}");
        }
    }

    void OnTriggerExit(Collider other) {
        this.GetComponent<MeshRenderer>().material.color = Color.grey;
    }

}
