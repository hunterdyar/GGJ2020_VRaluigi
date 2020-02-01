using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    int jumps;
    Vector3 vel;
    float resetAngle = 45;
    float timeSinceGrounded;
    float timeSinceJumpPressed;
    bool grounded;
    public float preCoyoteTime;
    public float postCoyoteTime;
    public LayerMask layerMask;
    public float gravity;
    public float movementSpeed;
    public float jumpForce;
    [Range(0,1)]
    public float friction;
    public bool wallsResetJumps = true;
    bool ignoreFriction = false;
    void Start()
    {
        grounded = false;
        timeSinceGrounded = 0;
        timeSinceJumpPressed = 0;
        jumps = 0;
        if(wallsResetJumps)
        {
            resetAngle = 145;
        }else{
            resetAngle = 45;
        }
    }

    public void Move(Vector2 dir)
    {
        dir.Normalize();
        //Set velocity x axis to movement speed????
        vel = new Vector3(dir.x*movementSpeed,vel.y,0);
        ignoreFriction = true;//we moved this frame.
    }
    public void Jump()
    {
        timeSinceJumpPressed = 0;

        if(grounded || timeSinceGrounded<postCoyoteTime)
        {
            vel = new Vector3(vel.x,jumpForce,0);
        }else{
            if(jumps<1)//jumps = air-jumps
            {
                vel = new Vector3(vel.x,jumpForce,0);
                jumps++;
            }
        }
    }
    void Update()
    {
        if(grounded)
        {
            jumps = 0;
        }
        timeSinceGrounded = timeSinceGrounded + Time.deltaTime;
        timeSinceJumpPressed = timeSinceJumpPressed + Time.deltaTime;
        //gravity Force
        vel = vel + new Vector3(0,-gravity,0)*Time.deltaTime;

        CheckCollision();//also friction

        //Move the player
        transform.position = transform.position+vel*Time.deltaTime;
    }
    void CheckCollision()
    {
        Vector3 nextFrame = vel*Time.deltaTime;
        RaycastHit hitInfo = new RaycastHit();
        if(Physics.BoxCast(transform.position,transform.localScale/2,nextFrame,out hitInfo,Quaternion.identity,nextFrame.magnitude,layerMask))
        {
            if(Vector3.Angle(Vector3.up,hitInfo.normal) < resetAngle)//<90 vertical, <180 wall-reset
            {
                grounded = true;
                timeSinceGrounded = 0;
                if(timeSinceJumpPressed < preCoyoteTime)
                {
                    //we meant to press the jump button JUST a moment ago, right before landing.
                    Jump();
                    return;//friction breaks the whole coyote time thing lol.
                }
            }else{
                grounded = false;
            }

            Vector3 antiForce = Vector3.Project(vel,hitInfo.normal);
            CheckFriction(hitInfo.normal);
            //
            vel = vel - antiForce;

        }else
        {
            grounded = false;
        }
    }
    void CheckFriction(Vector3 frictionNormal)
    {
        if(frictionNormal != Vector3.zero && !ignoreFriction)
        {
            if(Vector3.Dot(vel,frictionNormal) != 0)
            {
                //Generalized Friction
                Vector3 fricV = (Vector2)Vector3.ProjectOnPlane(vel,frictionNormal);
                if(fricV.normalized == Vector3.up || fricV.normalized == Vector3.down)
                {
                    //None of that vertical friction
                    fricV = Vector3.zero;
                }
                vel = vel-fricV*friction;
            }
        }
    }
    void LateUpdate()
    {
        ignoreFriction = false;
    }
}
