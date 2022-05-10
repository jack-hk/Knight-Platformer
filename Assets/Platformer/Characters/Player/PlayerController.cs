using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class PlayerController : EntityController
{
    SpriteRenderer entityRenderer;
    Rigidbody2D entityPhysics;

    private void Awake()
    {
        common = GameObject.Find("Controller").GetComponent<GameController>();

        entityRenderer = GetComponent<SpriteRenderer>();
        entityPhysics = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameObject != null)
        {
            if (health <= 0)
            {
                Death();
            }
        }
        ExitHurt(entityRenderer);
        common.playerHealth.text = "" + health;
        common.playerScore.text = "" + globalScore;
    }

    public override void OnHit(Collision2D contact)
    {
        if (contact.gameObject.CompareTag("Enemy") && !isDead)
        {
            health -= 10;
            if (!isHurt)
            {
                OnHurt(entityRenderer);
            }
        }
        
    }

    public override void Death()
    {
        isDead = true;
        entityRenderer.color = Color.red;
        entityPhysics.bodyType = RigidbodyType2D.Static;
        if (common.SecondsTimer(1))
        {
            common.deathScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
