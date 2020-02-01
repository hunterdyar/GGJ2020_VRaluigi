using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
public class BoundsSetter : MonoBehaviour
{
    public Transform player;
    public Camera cam2D;
    public Vector3Reference bounds;
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        bounds.Value = new Vector3(cam2D.ScreenToWorldPoint(Vector2.zero).x,player.position.x,cam2D.ScreenToWorldPoint(new Vector2(cam2D.pixelWidth,cam2D.pixelHeight)).x);
    }
}
