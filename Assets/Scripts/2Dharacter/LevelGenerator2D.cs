using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator2D : MonoBehaviour
{
    public LevelScriptableObj levelState;
    public GameObject blockPrefab;
    Dictionary<Vector3,GameObject> blocks;
    void Start()
    {
        blocks = new Dictionary<Vector3,GameObject>();
        //DEBUG
        levelState.generateRandomLayout();
        //
        CreateBlocks();
    }

    [ContextMenu("Create Blocks")]
    public void CreateBlocks()
    {
        //
        //
        int k = 0;//What depth is the level at?
        for (int i = 0; i < levelState.Row; i++)
        {
            for (int j = 0; j < levelState.Col; j++)
            {
                    Vector3 key = levelState.getCubePos(i, j, k);
                    if(blocks.ContainsKey(key))
                    {
                        //a block exists here.
                        //is it the right type of block?
                    }else
                    {
                        GameObject newBlock = Instantiate(blockPrefab, key, Quaternion.identity);
                        newBlock.transform.parent = this.transform;//SetParent?
                        blocks[key] = newBlock;//add to the dictionary
                    }
            }
        }
        //
        foreach(KeyValuePair<Vector3,GameObject> bl in blocks)
        {
            List<Vector3> removeMe = new List<Vector3>();
            //If there is no block at this position anymore, we need to destory it.
            if(!levelState.hasCube((int)bl.Key.x,(int)bl.Key.y,(int)bl.Key.z))
            {
                removeMe.Add(bl.Key);
            }
            foreach(Vector3 b1 in removeMe)
            {
                Destroy(blocks[b1]);
                blocks.Remove(b1);
            }
        }
    }
}
