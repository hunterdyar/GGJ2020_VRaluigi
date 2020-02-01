using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerController))]
public class PlatformerInput : MonoBehaviour
{

    PlatformerController pc;
    // Start is called before the first frame update
    void Awake()
    {
        pc = GetComponent<PlatformerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            pc.Jump();
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            pc.Move(Vector2.right);
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            pc.Move(Vector2.left);
        }
    }
}
