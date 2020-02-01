using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TableScriptableObject", order = 1)]
public class LevelScriptableObj : ScriptableObject
{
    public const int ROW = 10;
    public const int COL = 20;
    public const int DEP = 1;
    public const float cubeSize = 0.1f;

    public Transform Origin
    {
        get { return _origin; }
        set { _origin = value; }
    }

    public int Row
    {
        get
        {
            return ROW;
        }
    }

    public int Col
    {
        get
        {
            return COL;
        }
    }

    public int Depth
    {
        get
        {
            return DEP;
        }
    }

    public int[,] getLayout()
    {
        int[,] res = new int[ROW, COL];
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                res[i, j] = _voxelStates[i, j, _curDep];
            }
        }
        return res;
    }

    public Vector3 getCubePos(int row, int col, int dep)
    {
        return Origin.position + new Vector3(col * cubeSize, row * cubeSize, dep * cubeSize);
    }

    public bool hasCube(int row, int col, int dep)
    {
        return _voxelStates[row, col, dep] == 1;
    }

    // return row,col,dep of closest cube
    public int[] getClosestCube(Transform obj)
    {
        float minDist = 1000.0f;
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                for (int k = 0; k < DEP; k++)
                {
                    if (Vector3.Distance(obj.position, getCubePos(i,j,k)) < minDist) {

                    }

                }
            }
        }
        return new int[] { 0, 0, 0 };
    }

    public void generateRandomLayout()
    {
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                _voxelStates[i, j, 0] = Random.Range(0, 2);
            }
        }
    }


    private Transform _origin;
    private int _curDep = 0;
    private int[,,] _voxelStates = new int[ROW, COL, DEP];

}
