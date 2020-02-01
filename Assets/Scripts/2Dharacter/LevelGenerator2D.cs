using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator2D : MonoBehaviour
{
    public TableScriptableObj table;
    public GameObject blockPrefab;
    void Start()
    {
        //DEBUG
        table.generateRandomLayout();
        //
        GenerateLevel();
    }

    [ContextMenu("GenerateLevel")]
    void GenerateLevel()
    {
        //DEBUG
        table.generateRandomLayout();
        foreach(Transform c in transform)
        {
            DestroyImmediate(c.gameObject);//destroy all children NOW.
        }
        //
        table.generateCubes(transform,blockPrefab,1);
    }
}
