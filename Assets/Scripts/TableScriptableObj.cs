using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TableScriptableObject", order = 1)]
public class TableScriptableObj : ScriptableObject
{
    const int ROW = 10;
    const int COL = 20;
    const int DEP = 1;
    const float cubeSize = 0.1f;


    public TableScriptableObj()
    {
    }

    public int[,] getLayout()
    {
        int[,] res = new int[ROW, COL];
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                res[i, j] = voxelStates[i, j, curDep];
            }
        }
        return res;
    }

    public void generateRandomLayout()
    {
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                voxelStates[i, j, 0] = Random.Range(0, 2);
            }
        }
    }

    public void generateCubes(Transform origin, GameObject cube)
    {
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                for (int k = 0; k < DEP; k++)
                {
                    if (voxelStates[i, j, k] == 1)
                    {
                        var pos = origin.position + new Vector3(j * cubeSize, i * cubeSize, k * cubeSize);
                        var newCube = Instantiate(cube, pos, Quaternion.identity);
                        newCube.transform.parent = origin;
                    }
                }
            }
        }
    }

    private int curDep = 0;
    private int[,,] voxelStates = new int[ROW, COL, DEP];

}
