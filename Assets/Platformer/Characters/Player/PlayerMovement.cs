using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class PlayerMovement : MonoBehaviour
{
    // --------------Data-------------- 
    [SerializeField] private LayerMask surfaceLayer;

    public float walkSpeed;
    public float runSpeed;
    public float jumpVelocity;
    public float test;

    private bool isAttacking = false;
    private Vector2 facingDirection = Vector2.right;

    Vector2 entityMove;
    Rigidbody2D entityPhysics;
    CapsuleCollider2D entityCollider;
    Animator entityAnimator;
    SpriteRenderer entityRenderer;

    // --------------In-Built-------------- 
    private void Start()
    {
        entityPhysics = GetComponent<Rigidbody2D>();
        entityCollider = GetComponent<CapsuleCollider2D>();
        entityAnimator = GetComponent<Animator>();
        entityRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        int runtimeSeconds = runtimeSeconds + 1; //rename
        Move("walk");
        Attack();
    }
    private void Update()
    {
        entityMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Jump();
        
    }

    // --------------Functions-------------- 
    private void Jump()
    {
        entityAnimator.SetBool("isJumping", !IsGrounded());
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            entityPhysics.velocity = Vector2.up * jumpVelocity;
        }
    }

    /*
     * <params>
     * </params>
     * 
     */
    private void Move(string state) //state determines animations. ex: walking, running, powered-up, etc.
    {
        bool isPoweredUp = false;

        //check if looking backwards
        if (Input.GetAxisRaw("Horizontal") <= -1) 
        {
            entityRenderer.flipX = true;
            facingDirection = Vector2.left;
}
        else
        {
            entityRenderer.flipX = false;
            facingDirection = Vector2.right;
        }

        //run animation decided through velocity
        if (Mathf.Abs(entityPhysics.velocity.x) >= 6f) 
        {
            entityAnimator.SetFloat("animatorSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal")) + 1); //sets 'animatorSpeed' to 2 when running
        }
        else
        {
            entityAnimator.SetFloat("animatorSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal"))); //sets 'animatorSpeed' to 1 when walking
        }

        //sprint/powerup functionality
        if (Input.GetButton("Sprint") && !isPoweredUp) {
            state = "run";
            entityPhysics.velocity = new Vector2(entityMove.x * runSpeed * Time.deltaTime, entityPhysics.velocity.y); //movement

        }
        else if (isPoweredUp) {
            state = "power";
            entityPhysics.velocity = new Vector2(entityMove.x * runSpeed * Time.deltaTime, entityPhysics.velocity.y);
        }
        else
        {
            state = "walk";
            entityPhysics.velocity = new Vector2(entityMove.x * walkSpeed * Time.deltaTime, entityPhysics.velocity.y);
        }

    }

    private void Attack()
    {
        //ground attack
        if (Input.GetButton("Fire1") && !isAttacking && IsGrounded() && entityAnimator.GetInteger("isAttacking") == 0) 
        {
            entityAnimator.SetInteger("isAttacking", 1);
            //entityPhysics.AddForce(facingDirection * test, ForceMode2D.Impulse);
            isAttacking = true;
#if UNITY_EDITOR 
            Debug.Log("Player: Side Attack");
#endif
        } 
        // in-air down attack
        else if (Input.GetButtonDown("Fire1") && !isAttacking && !IsGrounded() && entityAnimator.GetInteger("isAttacking") == 0)
        {
            entityAnimator.SetInteger("isAttacking", 1);
            isAttacking = true;
#if UNITY_EDITOR
            Debug.Log("Player: Air Attack");
#endif
        }
       

        if (isAttacking && entityAnimator.GetInteger("isAttacking") > 0)
        {
            isAttacking = false;
        }
    }
   

    private bool IsGrounded()
    {
        float extraHeightTest = 0.2f; //additional raycast length for surface detection
        RaycastHit2D rayhit = Physics2D.Raycast(entityCollider.bounds.center, Vector2.down, entityCollider.bounds.extents.y + extraHeightTest, surfaceLayer);

#if UNITY_EDITOR //debugger visual ray
        Color rayColor;
        if (rayhit.collider != null)
        {
            rayColor = Color.green;
        } else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(entityCollider.bounds.center, Vector2.down * (entityCollider.bounds.extents.y + extraHeightTest), rayColor);
#endif
        return rayhit.collider != null;
    }
}
