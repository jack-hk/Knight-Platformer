using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // --------------Data-------------- 

    [SerializeField] private LayerMask SurfaceLayer;

    public float walkSpeed;
    public float runSpeed;
    public float jumpVelocity;

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
        Move("walk");
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

    private void Move(string state)
    {
        bool isPoweredUp = false;
        //check if looking backwards
        if (Input.GetAxisRaw("Horizontal") <= -1) 
        {
            entityRenderer.flipX = true;
        }
        else
        {
            entityRenderer.flipX = false;
        }

        if (Mathf.Abs(entityPhysics.velocity.x) >= 6f) //d3nny
        {
            entityAnimator.SetFloat("animatorSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal")) + 1); //sets 'animatorSpeed' to 2 when running
        }
        else
        {
            entityAnimator.SetFloat("animatorSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal"))); //sets 'animatorSpeed' to 1 when walking
        }

        if (Input.GetButton("Sprint") && !isPoweredUp) {
            state = "run";
            entityPhysics.velocity = new Vector2(entityMove.x * runSpeed * Time.deltaTime, entityPhysics.velocity.y); //movement

        }
        else
        {
            state = "walk";
            entityPhysics.velocity = new Vector2(entityMove.x * walkSpeed * Time.deltaTime, entityPhysics.velocity.y); //movement
        }

    }

    private bool IsGrounded()
    {
        float extraHeightTest = 0.2f; //additional raycast length for surface detection

        RaycastHit2D rayhit = Physics2D.Raycast(entityCollider.bounds.center, Vector2.down, entityCollider.bounds.extents.y + extraHeightTest, SurfaceLayer);
#if UNITY_EDITOR
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
