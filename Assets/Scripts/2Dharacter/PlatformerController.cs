using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
public class PlatformerController : MonoBehaviour
{
    int jumps;
    Vector3 vel;
    float resetAngle = 45;
    float timeSinceGrounded;
    float timeSinceJumpPressed;
    bool grounded;
    Vector3 newLevelPos;
    public Transform levelParent;
    public Vector2Reference playerPosition;
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
    Animator animator;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        playerPosition.Value = (Vector2)transform.position;
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

        if(dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }else if(dir.x > 0)
        {   spriteRenderer.flipX = false;

        }
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
        //Update position reference
        animator.SetFloat("speed",vel.magnitude);
        
        playerPosition.Value = playerPosition.Value+(Vector2)vel*Time.deltaTime;
        //Update player Y
        transform.position = new Vector3(playerPosition.Value.x,playerPosition.Value.y,transform.position.z);

    }
    void FixedUpdate()
    {
    //    levelParent.GetComponent<Rigidbody>().MovePosition(levelParent.position + Vector3.left*vel.x*Time.fixedDeltaTime);
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
                    StartCoroutine(JumpAtEndOfFrame());
                    
                }
            }else{
                grounded = false;
            }

            Vector3 antiForce = Vector3.Project(vel,hitInfo.normal);
            // if(!didCoyote)
            // {
                CheckFriction(hitInfo.normal);
            // }
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
        // newLevelPos = levelParent.position + Vector3.left*vel.x*Time.deltaTime;
        //levelParent.position = newLevelPos;
    }
    IEnumerator JumpAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        Jump();
    }
}
