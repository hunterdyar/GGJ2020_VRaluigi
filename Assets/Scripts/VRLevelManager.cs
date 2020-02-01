using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLevelManager : MonoBehaviour
{
    public TableScriptableObj levelState;

    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        levelState.Origin = this.transform;
        levelState.generateRandomLayout();
        _cubes = new GameObject[levelState.Row, levelState.Col, levelState.Depth];
        generateCubes();
        var layout = levelState.getLayout();
        /*
        for (int i = 0; i < layout.GetLength(0); i++)
        {
            for (int j = 0; j < layout.GetLength(1); j++)
            {
                Debug.Log($"row{i},col{j},cube{layout[i, j]}");
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void generateCubes()
    {
        for (int i = 0; i < levelState.Row; i++)
        {
            for (int j = 0; j < levelState.Col; j++)
            {
                for (int k = 0; k < levelState.Depth; k++)
                {
                    if (levelState.hasCube(i, j, k))
                    {
                        var pos = levelState.getCubePos(i, j, k);
                        _cubes[i, j, k] = Instantiate(cube, pos, Quaternion.identity);
                        _cubes[i, j, k].transform.parent = this.transform;
                    }
                }
            }
        }
    }

    private GameObject[,,] _cubes;

}
