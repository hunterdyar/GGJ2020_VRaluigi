using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
public class PlatformerController : MonoBehaviour
{
    int jumps;
    Vector2 vel;
    float resetAngle = 45;
    float timeSinceGrounded;
    float timeSinceJumpPressed;
    bool grounded;
    Vector3 newLevelPos;
    public GameEvent OnDeathEvent;
    public FloatReference furthestCameraLeftBound;
    public Vector2Reference playerPosition;
    public float preCoyoteTime;
    public float postCoyoteTime;
    public LayerMask layerMask;
    public float gravity;
    public float movementSpeed;
    public float jumpForce;
    public float deathHeightInWorldUnits;
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
        vel = new Vector2(dir.x*movementSpeed,vel.y);
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
            vel = new Vector2(vel.x,jumpForce);
        }else{
            if(jumps<1)//jumps = air-jumps
            {
                vel = new Vector2(vel.x,jumpForce);
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
        if(transform.position.y < deathHeightInWorldUnits)
        {
            if(OnDeathEvent != null){
                OnDeathEvent.Raise();
            }
        }
        timeSinceGrounded = timeSinceGrounded + Time.deltaTime;
        timeSinceJumpPressed = timeSinceJumpPressed + Time.deltaTime;
        //gravity Force
        vel = vel + new Vector2(0,-gravity)*Time.deltaTime;

        CheckCollisionOnAxis(Vector3.right);//also friction
        CheckCollisionOnAxis(Vector3.up);

        //Move the player
        //Update position reference
        animator.SetFloat("speed",vel.magnitude);
        if(playerPosition.Value.x>furthestCameraLeftBound.Value)
        {
            playerPosition.Value = playerPosition.Value+vel*Time.deltaTime;
        }else{
            if(vel.x < 0){
                playerPosition.Value = playerPosition.Value+Vector2.up*vel.y*Time.deltaTime;
            }else{
                //wait what
                playerPosition.Value = playerPosition.Value+vel*Time.deltaTime;
            }
        }
        
        //Update player Y
        transform.position = new Vector3(playerPosition.Value.x,playerPosition.Value.y,transform.position.z);

    }
    void CheckCollision()
    {
        Vector3 nextFrame = vel*Time.deltaTime;
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.BoxCast((Vector2)transform.position,new Vector2(transform.localScale.x*0.8f,transform.localScale.y),0,nextFrame.normalized,nextFrame.magnitude,layerMask);
        if(hitInfo.collider != null)
        {
            if(Vector2.Angle(Vector2.up,hitInfo.normal) < resetAngle)//<90 vertical, <180 wall-reset
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

            Vector3 antiForce = Vector3.Project((Vector3)vel,(Vector3)hitInfo.normal);
            // if(!didCoyote)
            // {
                CheckFriction(hitInfo.normal);
            // }
            //
            vel = vel - (Vector2)antiForce;
        }else
        {
            grounded = false;
        }
    }
    void CheckCollisionOnAxis(Vector3 dir)
    {
        Vector3 nextFrame = Vector3.Project((Vector3)vel,dir)*Time.deltaTime;
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.BoxCast((Vector2)transform.position,new Vector2(transform.localScale.x*0.8f,transform.localScale.y),0,nextFrame.normalized,nextFrame.magnitude,layerMask);
        if(hitInfo.collider != null)
        {
            if(Vector2.Angle(Vector2.up,hitInfo.normal) < resetAngle)//<90 vertical, <180 wall-reset
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

            Vector3 antiForce = Vector3.Project((Vector3)vel,(Vector3)hitInfo.normal);
            // if(!didCoyote)
            // {
                CheckFriction(hitInfo.normal);
            // }
            //
            vel = vel - (Vector2)antiForce;
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
                vel = vel-(Vector2)fricV*friction;
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
