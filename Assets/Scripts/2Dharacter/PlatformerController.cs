using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    int jumps;
    Vector2 vel;
    public LayerMask layerMask;
    public float gravity;
    public float movementSpeed;
    public float jumpForce;
    public float friction;
    void Start()
    {
        jumps = 0;
    }

    public void Move(Vector2 dir)
    {
        dir.Normalize();
        //Set velocity x axis to movement speed????
        vel = new Vector2(dir.x*movementSpeed,vel.y);
    }
    public void Jump()
    {
        if(jumps < 2)
        {
            vel = new Vector2(vel.x,jumpForce);
        }
    }
    void Update()
    {
        //gravity Force
        vel = vel + new Vector2(0,-gravity)*Time.deltaTime;

        CheckCollision();//also friction

        //Move the player
        transform.position = transform.position+new Vector3(vel.x,vel.y,0)*Time.deltaTime;
    }
    void CheckCollision()
    {
        Vector2 nextFrame = new Vector3(vel.x,vel.y)*Time.deltaTime;
        RaycastHit hitInfo = new RaycastHit();
        if(Physics.BoxCast(transform.position,transform.localScale/2,nextFrame,out hitInfo,Quaternion.identity,nextFrame.magnitude,layerMask))
        {
            Vector3 antiForce = Vector3.Project(vel,hitInfo.normal);
            CheckFriction(hitInfo.normal);
            //
            vel = vel - new Vector2(antiForce.x,antiForce.y);

        }
    }
    void CheckFriction(Vector3 frictionNormal)
    {
        if(frictionNormal != Vector3.zero)
        {
            if(Vector3.Dot(vel,frictionNormal) != 0)
            {
                vel = vel+ vel * Vector3.Dot(vel,frictionNormal)*friction;
            }
            // vel = vel - vel*friction*frictionAmount;
        }
    }
}
