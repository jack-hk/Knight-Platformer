using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // --------------Data-------------- 

    [SerializeField] private LayerMask SurfaceLayer;

    public float walkSpeed;
    public float jumpVelocity;

    Vector2 entityMove;
    Rigidbody2D entityPhysics;
    CapsuleCollider2D entityCollider;
    Animator entityAnimator;

    // --------------In-Built-------------- 

    private void Start()
    {
        entityPhysics = GetComponent<Rigidbody2D>();
        entityCollider = GetComponent<CapsuleCollider2D>();
        entityAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Walk();
        
    }
    private void Update()
    {
        entityMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Jump();
    }

    // --------------Functions-------------- 

    private void Jump()
    {
        if (IsGrounded() && Input.GetButton("Jump"))
        {
            entityPhysics.velocity = Vector2.up * jumpVelocity;
        }
    }

    private void Walk()
    {
        //entityPhysics.AddForce(entityMove * walkSpeed * Time.deltaTime, ForceMode2D.Force); //Force method
        entityPhysics.velocity = new Vector2(entityMove.x * walkSpeed * Time.deltaTime, entityPhysics.velocity.y);
        entityAnimator.SetFloat("speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
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
