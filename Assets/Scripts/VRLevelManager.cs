﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class VRLevelManager : MonoBehaviour
{
    public LevelScriptableObj levelState;
    public GameObject cube;
    public GameObject level;

    public GameEvent InitiateEvent;

    // player avatar movement
    public float scaleOffset;
    public Vector2Reference playerPosition;
    public Vector3 extraOffset;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        levelState.loadLayoutFromPrefab(level);
        levelState.Origin = this.transform;
        _cubes = new GameObject[levelState.Row, levelState.Col, levelState.Depth];
        levelState.generateRandomLayout();
        generateCubes();
        this.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        yield return new WaitForEndOfFrame();
        InitiateEvent.Raise();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move level oppsite to player's x postiion
        transform.position = extraOffset + new Vector3(-scaleOffset * playerPosition.Value.x, 0, 0);

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
                    //_cubes[i, j, k].transform.localScale = new Vector3(0.1f,0.1f,0.1f);

                    if (!levelState.hasCube(i, j, k))
                    {
                        _cubes[i, j, k].GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
        }
    }

    public void OnCubePlaced(GameObject cube)
    {
        var nearCube = levelState.getClosestCube(cube.transform);
        Debug.Log($"Near cube, x{nearCube[0]}, y{nearCube[1]}, z{nearCube[2]}");
        SetCube(nearCube[0], nearCube[1], nearCube[2]);

        var cubeState = cube.GetComponent<CubeState>();
        Debug.Log($"Cube released, x{cubeState.row}, y{cubeState.col}, z{cubeState.dep}");
        UnsetCube(cubeState.row, cubeState.col, cubeState.dep);
    }

    private void SetCube(int row, int col, int dep)
    {
        // reset cube transform
        _cubes[row, col, dep].transform.position = levelState.getCubePos(row, col, dep);
        _cubes[row, col, dep].transform.rotation = Quaternion.identity;
        // update levelState
        levelState.setCube(row, col, dep);
        // set visible
        _cubes[row, col, dep].GetComponent<MeshRenderer>().enabled = true;
    }

    private void UnsetCube(int row, int col, int dep)
    {
        // reset cube transform
        _cubes[row, col, dep].transform.position = levelState.getCubePos(row, col, dep);
        _cubes[row, col, dep].transform.rotation = Quaternion.identity;
        // update levelstate
        levelState.unsetCube(row, col, dep);
        // set invisible
        _cubes[row, col, dep].GetComponent<MeshRenderer>().enabled = false;
    }

    private void UpdatePlayerPos()
    {

    }

    private GameObject[,,] _cubes;

}
