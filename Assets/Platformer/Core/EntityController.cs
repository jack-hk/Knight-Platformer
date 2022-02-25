using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    private float time = 0.0f;
    private bool isMoving = false;
    private bool isJumpPressed = false;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); //gets the rigidbody from game object
    }

    void Update()
    {
        isJumpPressed = Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        if (isJumpPressed)
        {
            // the cube is going to move upwards in 10 units per second
            rigidbody2d.velocity = new Vector3(0, 10, 0);
            isMoving = true;
            Debug.Log("jump");
        }

        if (isMoving)
        {
            // when the cube has moved for 10 seconds, report its position
            time = time + Time.fixedDeltaTime;
            if (time > 10.0f)
            {
                Debug.Log(gameObject.transform.position.y + " : " + time);
                time = 0.0f;
            }
        }
    }
}


