using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLevelManager : MonoBehaviour
{
    public LevelScriptableObj levelState;

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
                    var pos = levelState.getCubePos(i, j, k);
                    _cubes[i, j, k] = Instantiate(cube, pos, Quaternion.identity);
                    _cubes[i, j, k].transform.parent = this.transform;
                    _cubes[i, j, k].GetComponent<CubeState>().row = i;
                    _cubes[i, j, k].GetComponent<CubeState>().col = j;
                    _cubes[i, j, k].GetComponent<CubeState>().dep = k;

                    if (levelState.hasCube(i, j, k))
                    {
                        _cubes[i, j, k].GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
        }
    }

    public void OnCubePlaced(GameObject cube)
    {
        Debug.Log("Cube placed");
        var cubeState = cube.GetComponent<CubeState>();
        Debug.Log($"x{cubeState.row}, y{cubeState.col}, z{cubeState.dep}");
        levelState.removeCube(cubeState.row, cubeState.col, cubeState.dep);
        cube.GetComponent<MeshRenderer>().enabled = false;
        var i = cubeState.lastCollided[0];
        var j = cubeState.lastCollided[1];
        var k = cubeState.lastCollided[2];
        levelState.addCube(cubeState.lastCollided[0], cubeState.lastCollided[1], cubeState.lastCollided[2]);
        _cubes[i,j,k].GetComponent<MeshRenderer>().enabled = true;
        
    }


    private GameObject[,,] _cubes;

}
