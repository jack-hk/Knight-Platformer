using UnityEngine;

//JackHK

public class PlayerMovement : MonoBehaviour
{
    // --------------Data-------------- 
    GameController game;
    AnimationController animator;
    [SerializeField] private GameObject controller;

    [Header("Configuration")]
    [SerializeField] private LayerMask surfaceLayer;

    public float walkSpeed;
    public float runSpeed;
    public float jumpVelocity;
<<<<<<< HEAD
    public float attackDashVelocity;

    private bool isAttacking = false;
    private Vector2 facingDirection = Vector2.right;
    private int lastDirection = 1;
=======
>>>>>>> parent of 66f051e (Merge branch 'main' of https://github.com/jack-hk/UnityPlatformerY1S2)

    Vector2 entityMove;
    Rigidbody2D entityPhysics;
    CapsuleCollider2D entityCollider;
    Animator entityAnimator;
    SpriteRenderer entityRenderer;

    // --------------In-Built-------------- 
    private void Start()
    {
        game = controller.GetComponent<GameController>();
        animator = controller.GetComponent<AnimationController>();

        entityPhysics = GetComponent<Rigidbody2D>();
        entityCollider = GetComponent<CapsuleCollider2D>();
        entityAnimator = GetComponent<Animator>();
        entityRenderer = GetComponent<SpriteRenderer>();

        animator.Play(Idle);
    }

    private void FixedUpdate()
    {
        Move("walk");
    }
    public void Update()
    {
        entityMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Jump();
<<<<<<< HEAD
        Attack();
=======
>>>>>>> parent of 66f051e (Merge branch 'main' of https://github.com/jack-hk/UnityPlatformerY1S2)
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

<<<<<<< HEAD
    private void Move(string state) //state determines animations. ex: walking, running, powered-up, etc.
=======
    private void Move(string state)
>>>>>>> parent of 66f051e (Merge branch 'main' of https://github.com/jack-hk/UnityPlatformerY1S2)
    {
        bool isPoweredUp = false;
        //check if looking backwards
        if (Input.GetAxisRaw("Horizontal") == -1) 
        {
            lastDirection = -1; 
            entityRenderer.flipX = true;
<<<<<<< HEAD
            facingDirection = Vector2.left;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            switch (lastDirection) //remember last direction
            {
                case -1:
                    entityRenderer.flipX = true;
                    facingDirection = Vector2.left;
                    break;
                case 0:
                    entityRenderer.flipX = false;
                    facingDirection = Vector2.right;
                    break;
                case 1:
                    entityRenderer.flipX = false;
                    facingDirection = Vector2.right;
                    break;
            }
=======
>>>>>>> parent of 66f051e (Merge branch 'main' of https://github.com/jack-hk/UnityPlatformerY1S2)
        }
        else
        {
            lastDirection = 1;
            entityRenderer.flipX = false;
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
        else
        {
            state = "walk";
<<<<<<< HEAD
            entityPhysics.velocity = new Vector2(entityMove.x * walkSpeed * Time.deltaTime, entityPhysics.velocity.y);
        }

    }

    private void Attack()
    {
        //ground attack
        if (Input.GetButtonDown("Fire1") && !isAttacking && IsGrounded() && entityAnimator.GetInteger("isAttacking") == 0) 
        {
            entityAnimator.SetInteger("isAttacking", 1);
            isAttacking = true;
#if UNITY_EDITOR
            Debug.Log("Player: Ground Attack");
#endif
        } 
        // in-air attack
        else if (Input.GetButtonDown("Fire1") && !isAttacking && !IsGrounded() && entityAnimator.GetInteger("isAttacking") == 0)
        {
            entityAnimator.SetInteger("isAttacking", 2);
            isAttacking = true;
#if UNITY_EDITOR
            Debug.Log("Player: Air Attack");
#endif
        }

        if (isAttacking)
        {
            if (game.SecondsTimer(0.01f)) //timer for small increments of addforce (dashing)
            {
                Dash(attackDashVelocity, 20);
            }
        }
    }

    private void Dash(float force, float durationInSeconds)
    {
            for (int i = 0; i < durationInSeconds; i++)
            {
                entityPhysics.AddForce(facingDirection * force);
            }
    }

    private void EndAttackAnimation() //used in animation events
    {
        entityAnimator.SetInteger("isAttacking", 0);
        isAttacking = false;
    }
=======
            entityPhysics.velocity = new Vector2(entityMove.x * walkSpeed * Time.deltaTime, entityPhysics.velocity.y); //movement
        }

    }
>>>>>>> parent of 66f051e (Merge branch 'main' of https://github.com/jack-hk/UnityPlatformerY1S2)

    private bool IsGrounded()
    {
        float extraHeightTest = 0.2f; //additional raycast length for surface detection
        RaycastHit2D rayhit = Physics2D.Raycast(entityCollider.bounds.center, Vector2.down, entityCollider.bounds.extents.y + extraHeightTest, surfaceLayer);

#if UNITY_EDITOR //visual ray for debugging
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
