using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
public class PlayerGhost : MonoBehaviour
{
    public float scaleOffset;
    public Transform levelParent;
    public Vector2Reference playerPosition;
    Vector3 offset;
    public Vector3 extraOffset;
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();//Wait until the player initiates.
        transform.position = (Vector3)playerPosition.Value;
        offset = levelParent.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = new Vector3(0, playerPosition.Value.y, 0);
        transform.position = extraOffset + pos * scaleOffset;
    }
}
