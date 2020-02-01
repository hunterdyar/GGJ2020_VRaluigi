using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
public class BoundsSetter : MonoBehaviour
{
    public Transform player;
    public Camera cam2D;
    public Vector3Reference bounds;
    public FloatReference furthestLeftCamBounds;
    void Start()
    {
        furthestLeftCamBounds.Value = -Mathf.Infinity;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if(cam2D.ScreenToWorldPoint(Vector2.zero).x>furthestLeftCamBounds.Value)
        {
            furthestLeftCamBounds.Value = cam2D.ScreenToWorldPoint(Vector2.zero).x;
        }

        bounds.Value = new Vector3(cam2D.ScreenToWorldPoint(Vector2.zero).x,player.position.x,cam2D.ScreenToWorldPoint(new Vector2(cam2D.pixelWidth,cam2D.pixelHeight)).x);
    }
}
