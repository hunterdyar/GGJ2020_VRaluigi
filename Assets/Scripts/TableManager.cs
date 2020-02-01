using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public TableScriptableObj tableState;

    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        //tableState = ScriptableObject.CreateInstance<TableScriptableObj>();
        tableState.generateRandomLayout();
        tableState.generateCubes(this.transform, cube);
        var layout = tableState.getLayout();
        for(int i = 0; i < layout.GetLength(0); i++) {
            for(int j = 0; j < layout.GetLength(1); j++) {
                Debug.Log($"row{i},col{j},cube{layout[i,j]}");
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
