using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK
public class AIController : MonoBehaviour
{
    // --------------Data-------------- 
    [SerializeField] private LayerMask surfaceLayer, playerLayer;

    private Vector2 facingDirection = Vector2.right;
    private int xAxis = 0, yAxis = 0;
    private string state;

    private Transform target, self;

    public float walkSpeed, chaseRange;
    public string enemyType;
    public float fixedRotation = 0;

    Vector2 enemyMove;
    Rigidbody2D enemyPhysics;
    CapsuleCollider2D enemyCollider;
    Animator enemyAnimator;
    SpriteRenderer enemyRenderer;


    // --------------In-Built-------------- 
    private void Awake()
    {
        enemyPhysics = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyRenderer = GetComponent<SpriteRenderer>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        self = gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (state == "fly")
        {
            self.eulerAngles = new Vector3(self.eulerAngles.x, fixedRotation, fixedRotation);
        }
    }

    // --------------Functions-------------- 
    private string Move()
    {
        if (enemyType == "Ground")
        {
            enemyPhysics.velocity = new Vector2(enemyMove.x * walkSpeed * Time.deltaTime, enemyPhysics.velocity.y); //movement
            if (state == "walk")
            {
                enemyAnimator.SetFloat("animatorSpeed", Mathf.Abs(1));
                enemyMove = new Vector2(xAxis, yAxis);
                if (self.position.x > target.position.x)
                {
                    xAxis = -1;
                    enemyRenderer.flipX = true;
                    facingDirection = Vector2.left;

                }
                else
                {
                    xAxis = 1;
                    enemyRenderer.flipX = false;
                    facingDirection = Vector2.right;
                }
            }
            if (InRange(chaseRange))
            {
                state = "walk";
                return "walk";
            }
            else
            {
                return "idle";
            }

        }
        else if (enemyType == "Air")
        {
            if (InRange(chaseRange))
            {
                state = "fly";
                enemyAnimator.SetFloat("animatorSpeed", Mathf.Abs(1));
                enemyMove = new Vector2(xAxis, yAxis);

                self.transform.position = Vector2.MoveTowards(transform.position, target.position, walkSpeed * Time.deltaTime);
                return "fly";
            }
            else
            {
                return "idle";
            }
        }
        else
        {
            Debug.LogError("INVAILD: enemy type not found");
            return "idle";
        }

    }

    private bool IsGrounded()
    {
        float extraHeightTest = 0.2f; //additional raycast length for surface detection
        RaycastHit2D rayhit = Physics2D.Raycast(enemyCollider.bounds.center, Vector2.down, enemyCollider.bounds.extents.y + extraHeightTest, surfaceLayer);

#if UNITY_EDITOR //debugger visual ray
        Color rayColor;
        if (rayhit.collider != null)
        {
            rayColor = Color.green;
            
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(enemyCollider.bounds.center, Vector2.down * (enemyCollider.bounds.extents.y + extraHeightTest), rayColor);
#endif
        return rayhit.collider != null;
    }

    private bool InRange(float chaseRange)
    {
       Collider2D range = Physics2D.OverlapCircle(enemyCollider.bounds.center, chaseRange, playerLayer);
       return range != null;
    }
}
